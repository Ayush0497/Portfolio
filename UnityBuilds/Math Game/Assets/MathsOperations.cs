using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MathsOperations : MonoBehaviour
{
    int min;
    int max;
    int firstrandomnumber;
    int secondrandomnumber;
    int questionLeft = 10;
    int percentage = 0;
    int correct = 0;

    public TextMeshProUGUI text1; //texts on Panel 2 i.e. min and max values
    public TextMeshProUGUI text2; //

    public TextMeshProUGUI text3; //texts on Panel 3 i.e. number being added/sub/multiplied
    public TextMeshProUGUI text4; //
    public TextMeshProUGUI percentageText;
    public TextMeshProUGUI correctText;
    public TextMeshProUGUI questionLeftText;

    public TextMeshProUGUI sign;

    public TMP_InputField input;

    public TMP_Dropdown dropdown;

    GameObject panel1, panel2;

    public CanvasGroup inputCanvas; //had to create this to disable input while in input field was in background

    public void onValueChange(int value)
    {
        if(value == 0)
        {
            sign.text = "+";
        }
        else if(value == 1)
        {
            sign.text = "-";
        }
        else if (value == 2)
        {
            sign.text = "*";
        }
    }

    private void setValues()
    {
        percentage = 0;
        correct = 0;
        questionLeft = 10;
        min = int.Parse(text1.text);
        max = int.Parse(text2.text);
        firstrandomnumber = Random.Range(min, max + 1);
        secondrandomnumber = Random.Range(min, max + 1);
        text3.text = firstrandomnumber.ToString();
        text4.text = secondrandomnumber.ToString();
        questionLeftText.text = "Question Left: " + questionLeft;
        correctText.text = "Correct:" + correct;
    }
    public void onClick()
    {
        inputCanvas.interactable = true;
        input.ActivateInputField();
        setValues();
        input.text = "";
    }

    public void selectNextValue()
    {
        input.ActivateInputField();
        firstrandomnumber = int.Parse(text3.text);
        secondrandomnumber = int.Parse(text4.text);
        if(!string.IsNullOrWhiteSpace(input.text))
        {
            validate(firstrandomnumber, secondrandomnumber, int.Parse(input.text));
        }
        else
        {
            validate(firstrandomnumber, secondrandomnumber, -9999999); //just validating random number which is not in range
        }
        min = int.Parse(text1.text);
        max = int.Parse(text2.text);
        firstrandomnumber = Random.Range(min, max + 1);
        secondrandomnumber = Random.Range(min, max + 1);
        text3.text = firstrandomnumber.ToString();
        text4.text = secondrandomnumber.ToString();
        input.text = "";
    }

    private void validate(int a, int b, int? c)
    {
        switch (dropdown.value)
        {
           case 0:
                if (a+b == c)
                {
                    questionLeft--;
                    percentage += 10;
                    correct++;
                    questionLeftText.text = "Question Left: " + questionLeft;
                    correctText.text = "Correct:" + correct;
                }
               else
               {
                    questionLeft--;
                    questionLeftText.text = "Question Left: " + questionLeft;
               }
           break;
          
           case 1:
              if(a-b == c)
              {
                    questionLeft--;
                    percentage += 10;
                    correct++;
                    questionLeftText.text = "Question Left: " + questionLeft;
                    correctText.text = "Correct:" + correct;
              }
              else
              {
                    questionLeft--;
                    questionLeftText.text = "Question Left: " + questionLeft;
              }
           break;

            case 2:
                if(a*b == c)
                {
                    questionLeft--;
                    correct++;
                    percentage += 10;
                    questionLeftText.text = "Question Left: " + questionLeft;
                    correctText.text = "Correct:" + correct;
                }
                else
                {
                    questionLeft--;
                    questionLeftText.text = "Question Left: " + questionLeft;
                }
            break;
        }
        if(questionLeft == 0)
        {
            inputCanvas.interactable = false;
            panel1 = GameObject.FindGameObjectWithTag("third");
            panel2 = GameObject.FindGameObjectWithTag("fourth");
            panel1.transform.localScale = new Vector3(0, 0, 0);
            panel2.transform.localScale = new Vector3(1, 1, 1);
            percentageText.text = "Your Score: " + percentage + "%";
            setValues();
        }
    }
}
