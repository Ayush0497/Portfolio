using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sliders : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Slider slider1;
    public Slider slider2;

    public void onSlidingSlider1(Single a)
    {
        if (a < slider2.value)
        {
            slider1.value = a;
            text.text = ((int)a).ToString();
        }
    }

    public void onSlidingSlider2(Single a)
    {
        if (a > slider1.value)
        {
            slider2.value = a;
            text.text = ((int)a).ToString();
        }
    }
}
