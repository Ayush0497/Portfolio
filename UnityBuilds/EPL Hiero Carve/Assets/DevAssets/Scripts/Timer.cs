using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] public TMP_Text countdownText;

    private float currentTime;
    [SerializeField] GameObject currentPanel;
    [SerializeField] GameObject endPanel;
    [SerializeField]
    private GameManager _thisManager, _otherManager1, _otherManager2;
    public bool TrackTime;

    private void OnEnable()
    {
        if (_thisManager.CoopEnabled && !_otherManager1.CoopEnabled && !_otherManager2.CoopEnabled)
        {
            if (GameObject.FindGameObjectWithTag("Coop"))
            {
                GameObject.FindGameObjectWithTag("Coop").GetComponent<Coop>().TrackTimer = this;
            }
            TrackTime = true;
            countdownText.text = "150";
            currentTime = 150.0f;
        }
        else if ((_thisManager.CoopEnabled && _otherManager1.CoopEnabled && !_otherManager2.CoopEnabled)
    ||
    (_thisManager.CoopEnabled && _otherManager1.CoopEnabled && _otherManager2.CoopEnabled)
    ||
    (_thisManager.CoopEnabled && !_otherManager1.CoopEnabled && _otherManager2.CoopEnabled))
        {
            if (!_thisManager.Timer.TrackTime && !_otherManager1.Timer.TrackTime && !_otherManager2.Timer.TrackTime)
            {
                if (GameObject.FindGameObjectWithTag("Coop"))
                {
                    GameObject.FindGameObjectWithTag("Coop").GetComponent<Coop>().TrackTimer = this;
                }
                TrackTime = true;
                countdownText.text = "150";
                currentTime = 150.0f;
            }
        }
        else if (_thisManager.CoopEnabled && !TrackTime)
        {
            if (GameObject.FindGameObjectWithTag("Coop"))
            {
                float time = GameObject.FindGameObjectWithTag("Coop").GetComponent<Coop>().Time;
                time = Mathf.Round(time);
                countdownText.text = time.ToString();
                currentTime = time;
            }
        }
        else
        {
            countdownText.text = "150";
            currentTime = 150.0f;
        }
    }

    private void Update()
    {
        StartTimer();
    }

    /// <summary>
    /// Starts the 5-minute countdown in milliseconds.
    /// </summary>
    private void StartTimer()
    {
        if (_thisManager.CoopEnabled && !TrackTime)
        {
            if (GameObject.FindGameObjectWithTag("Coop"))
            {
                float time = GameObject.FindGameObjectWithTag("Coop").GetComponent<Coop>().Time;
                time = Mathf.Round(time);
                countdownText.text = time.ToString();
                currentTime = time;
            }
        }
        else
        {
            if (currentTime > 0.0f)
            {
                currentTime = currentTime - Time.deltaTime;
                countdownText.text = (currentTime).ToString("F0");
            }
            if (TrackTime)
            {
                if (GameObject.FindGameObjectWithTag("Coop"))
                {
                    GameObject.FindGameObjectWithTag("Coop").GetComponent<Coop>().Time = currentTime;
                }
            }
        }
        if (currentTime <= 1.0f)
        {
            currentPanel.SetActive(false);
            endPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Decreases the timer by 5 seconds (5000 milliseconds).
    /// </summary>
    /// 
    public void DecreaseTime()
    {
        if (currentTime != 0)
        {
            if (_thisManager.CoopEnabled && !TrackTime)
            {
                if (GameObject.FindGameObjectWithTag("Coop"))
                {
                    GameObject.FindGameObjectWithTag("Coop").GetComponent<Coop>().TrackTimer.DecreaseTime();
                }
            }
            else
            {
                if (currentTime - 5.0f >= 0.0f)
                {
                    currentTime -= 5.0f;
                }
                else
                {
                    currentTime = 0.0f;
                }
            }
        }
    }

    public void IncreaseTime()
    {
        if (currentTime != 0)
        {
            if (_thisManager.CoopEnabled && !TrackTime)
            {
                if (GameObject.FindGameObjectWithTag("Coop"))
                {
                    if (currentTime + 8.0f <= 150.0f)
                    {
                        GameObject.FindGameObjectWithTag("Coop").GetComponent<Coop>().TrackTimer.currentTime += 8.0f;
                        countdownText.color = Color.green;
                        StartCoroutine(ChangeColor());
                    }
                    else
                    {
                        GameObject.FindGameObjectWithTag("Coop").GetComponent<Coop>().TrackTimer.currentTime = 150.0f;
                    }
                }
            }
            else
            {
                if (currentTime + 8.0f <= 150.0f)
                {
                    currentTime += 8.0f;
                    countdownText.color = Color.green;
                    StartCoroutine(ChangeColor());
                }
                else
                {
                    currentTime = 150.0f;
                }

                if (GameObject.FindGameObjectWithTag("Coop"))
                {
                    GameObject.FindGameObjectWithTag("Coop").GetComponent<Coop>().Time = currentTime;
                }
            }
        }
    }

    private IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(1.0f);
        if (countdownText != null)
        {
            countdownText.color = Color.white;
        }
    }

    private void OnDisable()
    {
        currentTime = 150.0f;
        TrackTime = false;
    }

    private void OnDestroy()
    {
        if (countdownText != null)
        {
            countdownText.color = Color.white;
        }
    }
}