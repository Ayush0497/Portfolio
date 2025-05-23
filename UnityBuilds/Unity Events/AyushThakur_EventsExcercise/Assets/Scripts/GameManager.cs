using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] GameObject[] coins;
    [SerializeField] GameObject spawnArea;
    [SerializeField] GameObject player;
    [SerializeField] TextMeshProUGUI EventsText;
    [SerializeField] TextMeshProUGUI Timer;
    [SerializeField] GameObject StartPanel;
    [SerializeField] GameObject RestartPanel;
    [SerializeField] TextMeshProUGUI RestartPanelScore;

    [SerializeField] private float gameTime = 60.02f;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        score = 0;
        MyEvents.spawnCoins.AddListener(SpawnCoins);
        MyEvents.updateScore.AddListener(UpdateScore);
        StartPanel.SetActive(true);
        RestartPanel.SetActive(false);
        MyEvents.disableInput.Invoke();       
    }

    private void Update()
    {
        scoreText.text = $"Score: {score}";
        gameTime = gameTime - Time.deltaTime;
        Timer.text = gameTime.ToString("F2");

        if(gameTime <= 0.0f)
        {
            RestartPanel.SetActive(true);
            MyEvents.disableInput.Invoke();
        }

        if(RestartPanel.gameObject.activeSelf)
        {
            RestartPanelScore.text = $"Score: {score}";
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void UpdateScore(int value)
    {
        EventsText.text = "Event Invoked: Update Score";
        score += value;
    }

    private void SpawnCoins()
    {
        if(gameTime > 0.0f)
        {
            EventsText.text = "Event Invoked: Spawn Coins";
            Bounds bounds = spawnArea.GetComponent<Collider>().bounds;

            Vector3 randomPosition;
            bool positionFound = false;
            int attempts = 0;
            const int maxAttempts = 10; // Limit the number of attempts to avoid an infinite loop

            while (!positionFound && attempts < maxAttempts)
            {
                randomPosition = new Vector3(
                    Random.Range(bounds.min.x, bounds.max.x),
                    bounds.center.y + 2,
                    Random.Range(bounds.min.z, bounds.max.z)
                );

                int groundLayerMask = LayerMask.GetMask("Ground");
                int obstacleLayerMask = ~groundLayerMask;

                // Check if the position is valid (not overlapping with other colliders)
                if (!Physics.CheckSphere(randomPosition, 1.0f, obstacleLayerMask))
                {
                    GameObject coinPrefab = coins[Random.Range(0, coins.Length)];

                    // Instantiate the coin and store reference
                    GameObject spawnedCoin = Instantiate(coinPrefab, randomPosition, Quaternion.identity);

                    // Check tag of the instantiated object, not the prefab
                    if (spawnedCoin.CompareTag("RedCoin"))
                    {
                        StartCoroutine(deactivateCoin(spawnedCoin));
                    }

                    break;
                }

                attempts++; // Increment attempts to prevent infinite loops
            }
        }  
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        StartPanel.SetActive(false);
        gameTime = 60.02f;
        MyEvents.enableInput.Invoke();
    }

    public void RestartGame()
    {
        score = 0;
        scoreText.text = $"Score: {score}";

        gameTime = 60.02f;
        Timer.text = gameTime.ToString("F2");

        foreach (GameObject coin in GameObject.FindGameObjectsWithTag("Coin"))
        {
            Destroy(coin);
        }

        foreach (GameObject coin in GameObject.FindGameObjectsWithTag("RedCoin"))
        {
            Destroy(coin);
        }
        SpawnCoins();
        RestartPanel.SetActive(false);

        MyEvents.enableInput.Invoke();
    }

    private IEnumerator deactivateCoin(GameObject coin)
    {
        yield return new WaitForSeconds(10);
        if(coin!=null && coin.activeInHierarchy)
        {
            Destroy(coin);
        }
    }
}
