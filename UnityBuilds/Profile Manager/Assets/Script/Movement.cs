using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float currentSpeed;
    [SerializeField] bool finished;


    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI maximumSpeed;
    public TextMeshProUGUI ReadyText;
    public GameObject readyPanel;
    public GameObject spawner;
    public GameObject saveGhostDataPanel;
    private float elapsedTime = 0.0f;

    public bool gameStart;


    public float score;
    public GhostDataContainer GhostDataContainer;


    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = 15.0f;
        currentSpeed = 0.0f;
        finished = false;
        score = 0;
        gameStart = false;
        speed.text = "Speed: 0 KMPH";
        maximumSpeed.text = "Max Speed: 150 KMPH";
        scoreText.text = "Time: 0";
        elapsedTime = 0.0f;
        saveGhostDataPanel.SetActive(false);
        GhostDataContainer = new GhostDataContainer();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 0.0f && elapsedTime < 1.0f)
        {
            ReadyText.text = "Ready!!";
        }
        else if (elapsedTime >= 1.0f && elapsedTime < 2.0f)
        {
            ReadyText.text = "Set!!";
        }
        else if (elapsedTime >= 2.0f && elapsedTime < 3.0f)
        {
            ReadyText.text = "Go!!";
        }

        if (elapsedTime >= 3.0f) // Start the game after "Go!!" is displayed
        {           
            gameStart = true;       
        }

        if (gameStart && !finished)
        {
            speed.text = $"Speed: {(int)(currentSpeed * 10.0f)} KMPH"; // Display as integer
            maximumSpeed.text = $"Max Speed: {(int)(maxSpeed * 10.0f)} KMPH"; // Display as integer
            scoreText.text = $"Time: {(elapsedTime-3.0f).ToString("F2")}"; //Just display 2 float digits
            score = elapsedTime - 3.0f;
            readyPanel.SetActive(false);
        }


        if (Input.GetKey(KeyCode.W) && gameStart)
        {
            if (currentSpeed < maxSpeed)
            {
                currentSpeed = currentSpeed + 0.03f;
            }
            transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed);
        }
        if (Input.GetKey(KeyCode.A) && gameStart)
        {
            transform.Translate(Vector3.left * Time.deltaTime * currentSpeed);
        }
        if (Input.GetKey(KeyCode.D) && gameStart)
        {
            transform.Translate(Vector3.right * Time.deltaTime * currentSpeed);
        }
        if (Input.GetKeyUp(KeyCode.W) && gameStart)
        {
            currentSpeed = 0;
        }
        if (finished)
        {
            gameStart = false;
            currentSpeed = 0;
            saveGhostDataPanel.SetActive(true);
        }

        GhostDataContainer.AddPosition(this.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Tire")
        {
            Destroy(other.gameObject);
            currentSpeed = currentSpeed / 2;
        }

        if(other.gameObject.tag == "FinishLine")
        {
            finished = true;
            gameStart = false;
        }

        if (other.gameObject.tag == "Booster")
        {
            maxSpeed = 20;
            Destroy(other.gameObject);
            StartCoroutine(resetBoostTimer());
        }
    }

    IEnumerator resetBoostTimer()
    {
        yield return new WaitForSeconds(2);
        maxSpeed = 15;
        if(currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }    
    }
}
