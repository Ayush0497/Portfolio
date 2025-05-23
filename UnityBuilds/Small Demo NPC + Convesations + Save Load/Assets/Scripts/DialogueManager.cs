using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI dialogueText;
    public Button[] optionButtons;  // Two buttons for options
    public Image backgroundImage;
    public Sprite initialBackground;
    public Sprite merchantBackground;
    public Sprite wandererBackground;

    // State tracking variables
    private bool hasMetMerchant = false;
    private bool hasAskedMerchantSell = false;
    private bool hasAskedAboutArea = false; // Track if the player has asked about the area
    private bool hasMetStranger = false;
    private bool hasAskedAboutMerchants = false; // Track if the player has asked about the number of merchants
    private bool hasAskedLocalLegend = false;
    private int playerGold = 0;

    private void Start()
    {
        StartInitialChoice();
    }

    private void StartInitialChoice()
    {
        SetBackground(initialBackground);
        dialogueText.text = "Do you want to meet the Merchant or the Stranger?";
        SetButtonLabels("Meet Merchant", "Meet Stranger");

        ClearListeners();
        optionButtons[0].onClick.AddListener(StartMerchantDialogue);
        optionButtons[1].onClick.AddListener(StartStrangerDialogue);
        ShowButtons();
    }

    // ========== MERCHANT DIALOGUE ==========

    private void StartMerchantDialogue()
    {
        ClearListeners();
        SetBackground(merchantBackground);

        if (!hasMetMerchant)
        {
            dialogueText.text = "Ah, a new face! Welcome to my humble shop. Perhaps you're in need of something special?";
            SetButtonLabels("What do you sell?", "Ask about the area.");
            optionButtons[0].onClick.AddListener(MerchantSell);
            optionButtons[1].onClick.AddListener(AskAboutArea);
        }
        else
        {
            dialogueText.text = "Oh, you're back! I've got new items since we last met.";
            SetButtonLabels(hasAskedMerchantSell? "Tell me about your magical items" : "What do you sell?", hasAskedAboutArea ? "What do you think of the Stranger?" : "Ask about the area.");
            optionButtons[0].onClick.AddListener(hasAskedMerchantSell? MerchantMagicalItems : MerchantSell);
            optionButtons[1].onClick.AddListener(hasAskedAboutArea ? AskAboutStranger : AskAboutArea);
        }
    }

    private void MerchantSell()
    {
        dialogueText.text = "I sell trinkets, potions, and various magical items. Here, take this gold coin as a welcome gift.";
        playerGold += 1;
        Debug.Log($"Player received 1 gold coin. Total Gold: {playerGold}");
        hasMetMerchant = true;
        hasAskedMerchantSell = true;
        SetButtonLabels("Thanks!", "Goodbye");
        ClearListeners();
        optionButtons[0].onClick.AddListener(ReturnToInitialChoice);
        optionButtons[1].onClick.AddListener(ReturnToInitialChoice);
    }

    private void MerchantMagicalItems()
    {
        dialogueText.text = "I have a variety of magical items, including potions that can enhance your abilities and artifacts that may aid you in your journey.";
        SetButtonLabels("Thanks!", "Goodbye");
        ClearListeners();
        optionButtons[0].onClick.AddListener(ReturnToInitialChoice);
        optionButtons[1].onClick.AddListener(ReturnToInitialChoice);
    }

    private void AskAboutStranger()
    {
        dialogueText.text = "The Stranger is a mysterious figure who wanders these parts. They know a lot about the area but are often secretive.";
        SetButtonLabels("Thanks!", "Goodbye");
        ClearListeners();
        optionButtons[0].onClick.AddListener(ReturnToInitialChoice);
        optionButtons[1].onClick.AddListener(ReturnToInitialChoice);
    }

    private void AskAboutArea()
    {
        dialogueText.text = "The area around here is rich in resources and magic. Many adventurers pass through, seeking treasures and knowledge. To the north lies a vast forest filled with wonders, but also dangers. Make sure to explore carefully!";
        hasAskedAboutArea = true;
        hasMetMerchant = true;

        SetButtonLabels("Thanks!", "Goodbye");
        ClearListeners();
        optionButtons[0].onClick.AddListener(ReturnToInitialChoice);
        optionButtons[1].onClick.AddListener(ReturnToInitialChoice);
    }

    // ========== STRANGER DIALOGUE ==========

    private void StartStrangerDialogue()
    {
        ClearListeners();
        SetBackground(wandererBackground); // Set the wanderer background

        if (!hasMetStranger)
        {
            dialogueText.text = "Ah, a traveler! What brings you to these parts?";
            SetButtonLabels("Do you know any local legends?", "How many merchants do we have around here?");
            optionButtons[0].onClick.AddListener(HandleLocalLegends);
            optionButtons[1].onClick.AddListener(HandleMerchantCount);
            hasMetStranger = true; // Mark as met for future interactions
        }
        else
        {
            dialogueText.text = "Ah, back again! You seem to know your way around.";

            SetButtonLabels(hasAskedLocalLegend? "Can you tell me more about this place?": "Do you know any local legends?", hasAskedAboutMerchants ? "What do you think of the Merchant?" : "How many merchants do we have around here?");
            optionButtons[0].onClick.AddListener(hasAskedLocalLegend? StrangerInsight : HandleLocalLegends);
            optionButtons[1].onClick.AddListener(hasAskedAboutMerchants ? AskAboutMerchantOpinion : HandleMerchantCount);
        }
    }

    private void AskAboutMerchantOpinion()
    {
        dialogueText.text = "The Merchant is shrewd but trustworthy. Their magical items are genuine, but always negotiate the price!";
        SetButtonLabels("Thanks!", "Goodbye");
        ClearListeners();
        optionButtons[0].onClick.AddListener(ReturnToInitialChoice);
        optionButtons[1].onClick.AddListener(ReturnToInitialChoice);
    }

    private void HandleLocalLegends()
    {
        dialogueText.text = "Ah, local legends! One tale speaks of a hidden treasure buried deep within the Whispering Woods, guarded by an ancient spirit.";
        SetButtonLabels("Thanks", "Goodbye");
        hasAskedLocalLegend = true;
        ClearListeners();
        optionButtons[0].onClick.AddListener(ReturnToInitialChoice);
        optionButtons[1].onClick.AddListener(ReturnToInitialChoice);
    }

    private void HandleMerchantCount()
    {
        dialogueText.text = "There are a few merchants around here, each with unique specialties. The Merchant near the crossroads is known for magical artifacts, while the one by the river offers rare potions made from local herbs.";
        
        // Update the flag indicating the player has asked about the number of merchants
        hasAskedAboutMerchants = true; // Mark that the player asked about the merchants

        SetButtonLabels("Thanks", "Goodbye");
        ClearListeners();
        optionButtons[0].onClick.AddListener(ReturnToInitialChoice);
        optionButtons[1].onClick.AddListener(ReturnToInitialChoice);
    }

    private void StrangerInsight()
    {
        dialogueText.text = "The area has many secrets. The forest holds ancient ruins, and the mountains are rich in ores. But beware, not everything is as it seems!";
        SetButtonLabels("Thanks!", "Goodbye");
        ClearListeners();
        optionButtons[0].onClick.AddListener(ReturnToInitialChoice);
        optionButtons[1].onClick.AddListener(ReturnToInitialChoice);
    }

    // ========== UTILITY METHODS ==========

    private void SetBackground(Sprite background) => backgroundImage.sprite = background;
    private void ReturnToInitialChoice() => StartInitialChoice();

    private void SetButtonLabels(string label1, string label2)
    {
        optionButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = label1;
        optionButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = label2;
    }

    private void ShowButtons()
    {
        foreach (Button button in optionButtons) button.gameObject.SetActive(true);
    }

    private void ClearListeners()
    {
        foreach (Button button in optionButtons) button.onClick.RemoveAllListeners();
    }
}
