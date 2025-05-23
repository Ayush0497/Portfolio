using TMPro;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public TextMeshProUGUI minimumText;
    public TextMeshProUGUI maximumText;

    public void decrementMax()
    {
        int minValue = int.Parse(minimumText.text);
        int maxValue = int.Parse(maximumText.text);

        if (maxValue > 1 && maxValue > minValue+1)
        {
            maxValue--;
            maximumText.text = maxValue.ToString();
        }
    }

    public void incrementMax()
    {
        int minValue = int.Parse(minimumText.text);
        int maxValue = int.Parse(maximumText.text);

        if (maxValue < 100 && maxValue > minValue)
        {
            maxValue++;
            maximumText.text = maxValue.ToString();
        }
    }

    public void decrementMin()
    {
        int minValue = int.Parse(minimumText.text);
        int maxValue = int.Parse(maximumText.text);

        if (minValue > 1 && minValue < maxValue)
        {
            minValue--;
            minimumText.text = minValue.ToString();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void incrementMin()
    {
        int minValue = int.Parse(minimumText.text);
        int maxValue = int.Parse(maximumText.text);

        if (minValue < 100 && minValue < maxValue-1)
        {
            minValue++;
            minimumText.text = minValue.ToString();
        }
    }
}