using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Penalty : MonoBehaviour
{
    Timer timer;
    private bool deductTime;
    private Tilemap tilemap;
    private Animator animator;

    private void OnEnable()
    {
        tilemap = GetComponent<Tilemap>();
        float alpha = 0.0f;
        tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, alpha);
        deductTime = false;
        StartCoroutine(disableTimeDeduction(2.0f));
        if (FindGameShapeIsIn("MainGameLeft"))
        {
            timer = GameObject.FindGameObjectWithTag("TimerLeft").GetComponentInParent<Timer>();
            animator = GameObject.FindGameObjectWithTag("MainGameLeft").GetComponent<Animator>();
        }

        if (FindGameShapeIsIn("MainGameCenter"))
        {
            timer = GameObject.FindGameObjectWithTag("TimerCenter").GetComponentInParent<Timer>();
            animator = GameObject.FindGameObjectWithTag("MainGameCenter").GetComponent<Animator>();
        }

        if (FindGameShapeIsIn("MainGameRight"))
        {
            timer = GameObject.FindGameObjectWithTag("TimerRight").GetComponentInParent<Timer>();
            animator = GameObject.FindGameObjectWithTag("MainGameRight").GetComponent<Animator>();
        }
        if (animator != null)
        {
            animator.Play("Penalty", 0, 3f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Brush" && deductTime)
        {
            timer.DecreaseTime();
            animator.Play("Penalty", 0, 0f);
            if (tilemap!= null)
            {
                StartCoroutine(FadeOutAlpha(2.0f));
            }
            timer.countdownText.color = Color.red;
            deductTime = false;
            StartCoroutine(disableTimeDeduction(2.0f));
        }
    }
    private bool FindGameShapeIsIn(string gameTag)
    {
        GameObject go = GameObject.FindGameObjectWithTag(gameTag);
        if (go != null)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(
                go.GetComponent<RectTransform>(),
                Camera.main.WorldToScreenPoint(transform.position),
                Camera.main);
        }
        return false;
    }
    private IEnumerator FadeOutAlpha(float duration)
    {
        float elapsed = 0f;
        tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, 1.0f);
        Color originalColor = tilemap.color;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(originalColor.a, 0f, elapsed / duration);
            tilemap.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        tilemap.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    private IEnumerator disableTimeDeduction(float time)
    {
        yield return new WaitForSeconds(time);
        if (timer.countdownText != null)
        {
            timer.countdownText.color = Color.white;
        }
        deductTime = true;
    }
    void OnDisable()
    {
        if(timer != null && timer.countdownText != null && timer.countdownText.color != Color.green)
        {
            ResetTimerColor();
        }
        timer = null;
        deductTime = false;
    }

    private void ResetTimerColor()
    {
        if (timer != null && timer.countdownText != null)
        {
            timer.countdownText.color = Color.white;
        }
    }

    void OnDestroy()
    {
        ResetTimerColor();
    }
}