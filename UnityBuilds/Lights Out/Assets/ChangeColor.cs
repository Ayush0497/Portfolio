using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeColors : MonoBehaviour
{
    public Button[] buttons;
    private Button buttonOnRight;
    private Button buttonOnLeft;
    private Button buttonOnUp;
    private Button buttonOnDown;
    public TextMeshProUGUI winMessage;

    private bool[] lightActive = new bool[9];

    private void Start()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void changeColor(int i)
    {
        if (lightActive[i] == false)
        {
            lightActive[i] = true;
        }
        else
        {
            lightActive[i] = false;
        }

        buttons[i].image.color = Color.yellow;

        buttonOnRight = (Button)buttons[i].GetComponent<Button>().FindSelectableOnRight();
        buttonOnLeft = (Button)buttons[i].GetComponent<Button>().FindSelectableOnLeft();
        buttonOnDown = (Button)buttons[i].GetComponent<Button>().FindSelectableOnDown();
        buttonOnUp = (Button)buttons[i].GetComponent<Button>().FindSelectableOnUp();

        UpdateButtonState(buttonOnRight, i + 1);
        UpdateButtonState(buttonOnLeft, i - 1);
        UpdateButtonState(buttonOnDown, i + 3);
        UpdateButtonState(buttonOnUp, i - 3);

        for (int j = 0; j < buttons.Length; j++)
        {
            if (lightActive[j])
            {
                buttons[j].image.color = Color.yellow;
            }
            else
            {
                buttons[j].image.color = Color.black;
            }
        }
        CheckForWin();
    }

    private void UpdateButtonState(Button button, int index)
    {
        if (index >= 0 && index < lightActive.Length)
        {
            if (button != null)
            {
                if (lightActive[index])
                {
                    lightActive[index] = false; 
                }
                else
                {
                    lightActive[index] = true; 
                }
            }
        }
    }

    public void StartGame()
    {
            winMessage.text = "Lights Out";
            for (int j = 0; j < buttons.Length; j++)
            {
            buttons[j].enabled = true;
            lightActive[j] = false;
            }

            int a = Random.Range(0, 8);
            int b = Random.Range(0, 8);
            int c = Random.Range(0, 8);

            // Set the corresponding buttons as active
            lightActive[a] = true;
            lightActive[b] = true;
            lightActive[c] = true;

            for (int j = 0; j < buttons.Length; j++)
            {
                if (lightActive[j])
                {
                    buttons[j].image.color = Color.yellow;
                }
                else
                {
                    buttons[j].image.color = Color.black;
                }
            }
    }

    private void CheckForWin()
    {
        bool allOff = true;
        for (int i = 0; i < lightActive.Length; i++)
        {
            if (lightActive[i])
            {
                allOff = false;
                break;
            }
        }

        if (allOff)
        {
            winMessage.text = "You Win!";
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].enabled = false;
            }
        }
    }

    public void exit()
    {
        Application.Quit();
    }
}
