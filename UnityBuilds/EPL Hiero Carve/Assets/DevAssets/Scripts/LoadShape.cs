using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.U2D;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;

public class LoadShape : MonoBehaviour
{
    public Transform shapeHolder;
    public GameManager gameManager; // Reference to GameManager
    public TextMeshProUGUI wordDisplay;
    public List<Image> letterSlots; // UI image slots for displaying glyphs
    public List<TextMeshProUGUI> textSlots; // UI text slots for displaying English letters
    public List<GameObject> wispSlots; // wisp slots for displaying wisp animations
    [SerializeField] Timer timer;

    private GameObject shapeObject;
    private Dictionary<char, Sprite> shapeDictionary = new Dictionary<char, Sprite>(); // Dictionary for quick lookup

    [SerializeField]
    private string currentWord;
    [SerializeField]
    private int currentLetterIndex;

    [SerializeField]
    private Dictionary<char, GameObject> tilemapPrefabDictionary = new Dictionary<char, GameObject>();
    private Dictionary<char, GameObject> tilemapPenaltyPrefabDictionary = new Dictionary<char, GameObject>();
    private GameObject currentTileMapGlyphInstance;


    private GameObject currentPenaltyTileMapInstance; //tracks current Penalty Tilemap

    private bool isLoadingNewWord = false;
    private bool isWaitingForNextLetter = false;

    void OnEnable()
    {
        foreach(GameObject wisp in wispSlots)
        {
            wisp.SetActive(false);
        }
    }
    private void Start()
    {
        LoadShapesFromResources();  // Load all glyphs dynamically
        LoadTilemapPrefabsFromResources();
        SetCurrentWord();
        wordDisplay.text = currentWord; // Display the word
        LoadGlyphsAndLetters();
        currentLetterIndex = 0;
        InstantiateNextLetter();
    }

    public void ResetGame()
    {
        SetCurrentWord();
        wordDisplay.text = currentWord;

        currentLetterIndex = 0;
        isLoadingNewWord = false;

        ResetLetterHolders();
        LoadGlyphsAndLetters();

        if (currentTileMapGlyphInstance != null)
        {
            Destroy(currentTileMapGlyphInstance);
            currentTileMapGlyphInstance = null;
        }

        if (currentPenaltyTileMapInstance != null)
        {
            Destroy(currentPenaltyTileMapInstance);
        }
        InstantiateNextLetter();
    }

    private void LoadShapesFromResources()
    {
        shapeDictionary.Clear();
        Sprite[] loadedSprites = Resources.LoadAll<Sprite>("ArtAssets/Glyphs/Glyph");
        foreach (Sprite sprite in loadedSprites)
        {
            string spriteName = sprite.name;
            if (spriteName.Length > 2 && spriteName[1] == '_')
            {
                char letter = spriteName[0];
                shapeDictionary[char.ToUpper(letter)] = sprite;
            }
        }
    }

    private void LoadTilemapPrefabsFromResources()
    {
        tilemapPrefabDictionary.Clear();
        GameObject[] loadedPrefabs = Resources.LoadAll<GameObject>("ArtAssets/FinalAssets");
        foreach (GameObject prefab in loadedPrefabs)
        {
            char letter = prefab.name[0];
            tilemapPrefabDictionary[char.ToUpper(letter)] = prefab;
        }

        GameObject[] loadedPenaltyPrefabs = Resources.LoadAll<GameObject>("ArtAssets/Penalty"); //Load Penalty Tilemaps
        foreach (GameObject prefab in loadedPenaltyPrefabs)
        {
            char letter = prefab.name[0];
            tilemapPenaltyPrefabDictionary[char.ToUpper(letter)] = prefab;
        }
    }

    private void SetCurrentWord()
    {
        Transform parent = transform.parent;
        if (parent != null)
        {
            gameManager.GetGameWord();
            currentWord = gameManager.MyGameData.GetGameWord();
        }
    }

    private void LoadGlyphsAndLetters()
    {
        for (int i = 0; i < letterSlots.Count; i++)
        {
            if (i < currentWord.Length)
            {
                char letter = currentWord[i];
                Sprite letterShape = GetShapeForLetter(letter);
                letterSlots[i].sprite = letterShape;
                letterSlots[i].enabled = true;
                textSlots[i].text = letter.ToString().ToUpper();
                textSlots[i].enabled = false; // Hide text initially
            }
            else
            {
                letterSlots[i].enabled = false;
                textSlots[i].enabled = false;
            }
        }
    }

    private Sprite GetShapeForLetter(char letter)
    {
        shapeDictionary.TryGetValue(char.ToUpper(letter), out Sprite sprite);
        return sprite;
    }

    private void LoadNextLetter()
    {
        if (currentLetterIndex < 4)
        {
            ActivateWispSlot();

            // Trigger the animation
            Animator animator = wispSlots[currentLetterIndex].GetComponent<Animator>();

            // Start coroutine to reveal letter after animation ends
            StartCoroutine(RevealLetterAfterAnimation(animator, currentLetterIndex));

            currentLetterIndex++;

            // Instantiate next letter shape immediately
            if (currentLetterIndex < currentWord.Length && currentLetterIndex < letterSlots.Count)
            {
                InstantiateNextLetter();
            }
            else if (!isLoadingNewWord)
            {
                isLoadingNewWord = true;
                gameManager.MyGameData.SetWordsComplete(gameManager.MyGameData.GetWordsComplete() + 1);
                timer.IncreaseTime();
                StartCoroutine(WaitAndLoadNextWord(3.0f));
                if (currentPenaltyTileMapInstance != null)
                {
                    currentPenaltyTileMapInstance.SetActive(false);
                }
            }
        }
    }


    private IEnumerator RevealLetterAfterAnimation(Animator animator, int letterIndex)
    {
        // Wait for the animation to finish
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }

        // Then reveal the actual letter
        if (letterIndex < textSlots.Count)
        {
            textSlots[letterIndex].enabled = true;
            textSlots[letterIndex].GetComponentInChildren<ParticleSystem>().Play();
            letterSlots[letterIndex].enabled = false;
        }
    }


    private void InstantiateNextLetter()
    {
        if (currentLetterIndex < currentWord.Length)
        {
            // Destroy existing instances to prevent memory leaks
            if (currentTileMapGlyphInstance != null)
            {
                Destroy(currentTileMapGlyphInstance);
            }
            if (currentPenaltyTileMapInstance != null)
            {
                Destroy(currentPenaltyTileMapInstance);
            }

            char letter = currentWord[currentLetterIndex];

            // Instantiate the main tilemap glyph
            if (tilemapPrefabDictionary.TryGetValue(char.ToUpper(letter), out GameObject tilemapGlyph))
            {
                currentTileMapGlyphInstance = Instantiate(tilemapGlyph, shapeHolder);
                currentTileMapGlyphInstance.transform.SetAsFirstSibling();
                currentTileMapGlyphInstance.transform.localScale = new Vector3(150f, 50f, 50f);
                TileDetection tileDetection = currentTileMapGlyphInstance.GetComponentInChildren<TileDetection>();
                if (tileDetection != null)
                {
                    tileDetection.OnShapeCompleted += LoadNextLetter;
                }
            }

            // Instantiate the penalty glyph separately
            if (tilemapPenaltyPrefabDictionary.TryGetValue(char.ToUpper(letter), out GameObject tilemapPenaltyGlyph))
            {
                currentPenaltyTileMapInstance = Instantiate(tilemapPenaltyGlyph, shapeHolder);
                currentPenaltyTileMapInstance.transform.SetAsLastSibling();
                currentPenaltyTileMapInstance.transform.localScale = new Vector3(150f, 50f, 50f);
            }
        }
    }

    private void ActivateWispSlot()
    {
        for (int i = 0; i < wispSlots.Count; i++)
        {
            wispSlots[i].SetActive(i == currentLetterIndex);
        }
    }
    private void ResetLetterHolders()
    {
        for (int i = 0; i < letterSlots.Count; i++)
        {
            letterSlots[i].enabled = true;
            textSlots[i].enabled = true;
            foreach (TextMeshProUGUI textslots in textSlots)
            {
                textslots.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
            }
        }
    }

    IEnumerator WaitAndLoadNextWord(float delayPeriod)
    {
        yield return new WaitForSeconds(delayPeriod); // Wait before proceeding
        ResetLetterHolders();
        gameManager.GetGameWord();
        currentWord = gameManager.MyGameData.GetGameWord();
        currentLetterIndex = 0;
        LoadGlyphsAndLetters();
        InstantiateNextLetter();
        isLoadingNewWord = false; // Reset flag after loading new word
    }

    private void OnDisable()
    {
        currentLetterIndex = 0;
    }

}