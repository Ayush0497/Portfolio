using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Your AI will need to access the GameManager script on the object in the scene.
// You will have access to the following methods:
// void RollDice(): This method with roll the dice for a couple seconds.
// bool IsRolling(): This method will return a bool that tells you if the dice are currently rolling.
// void SetComboActive(int index, bool state): This method will set the interactable state of a combo button at a specified index. It will only do this if the combo has not yet been selected.
// void SelectCombo(int index): This will try to select the combo by index. You can use the enum DiceCombos and cast it to an int. eg. (int)GameManager.DiceCombos.LargeStraight
// void KeepDie(int index): This will toggle the keep button at the index.
// void GetDiceValues(ref int[] values): This will point the array given to the diceValues in the GameManager.
// bool IsComboSelected(int index): This will return if the combo has been selected 


public class AITemplate : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    [SerializeField] AIStates currentState = AIStates.RollDice1;

    [SerializeField] int[] frequency = new int[6];
    [SerializeField] int[] diceValues = new int[5];

    [SerializeField] Button aiButton;

    [SerializeField] bool twoPair = false;
    [SerializeField] bool threeOfAKind = false;
    [SerializeField] bool fourOfAKind = false;
    [SerializeField] bool fullHouse = false;
    [SerializeField] bool smallStraight = false;
    [SerializeField] bool largeStraight = false;
    enum AIStates
    {
        RollDice1,
        EvaluateDice1,
        KeepDice,
        RollDice2,
        EvaluateDice2
    }

    private void Update()
    {
        if (gameManager.IsRolling())
        {
            aiButton.interactable = false;
        }
        else
        {
            aiButton.interactable = true;
        }
    }

    public void TakeAIStep()
    {
        switch (currentState)
        {
            case AIStates.RollDice1:
                UpdateComboButtons();
                for (int i = 0; i < 6; i++)
                {
                    gameManager.SetComboActive(i, false);
                }
                // Call the RollDice method on the gameManager.
                gameManager.RollDice();
                currentState = AIStates.EvaluateDice1;
                aiButton.GetComponentInChildren<TMP_Text>().text = "Eval Combos";
                break;

            case AIStates.EvaluateDice1:
                gameManager.GetDiceValues(ref diceValues);
                CheckCombos();
                currentState = AIStates.KeepDice;
                aiButton.GetComponentInChildren<TMP_Text>().text = "Keep Dice";
                if ((fullHouse && !gameManager.IsComboSelected(3)) || (largeStraight && !gameManager.IsComboSelected(5))) //check for full house and large straights on first roll
                {

                    currentState = AIStates.RollDice1;
                    aiButton.GetComponentInChildren<TMP_Text>().text = "First Roll";
                }
                // Check if the last combination is required and we get it, then don't roll
                if (
                    (twoPair && !gameManager.IsComboSelected(0) &&
                        gameManager.IsComboSelected(1) &&
                        gameManager.IsComboSelected(2) &&
                        gameManager.IsComboSelected(3) &&
                        gameManager.IsComboSelected(4) &&
                        gameManager.IsComboSelected(5)) ||

                    (threeOfAKind && !gameManager.IsComboSelected(1) &&
                        gameManager.IsComboSelected(0) &&
                        gameManager.IsComboSelected(2) &&
                        gameManager.IsComboSelected(3) &&
                        gameManager.IsComboSelected(4) &&
                        gameManager.IsComboSelected(5)) ||

                    (fourOfAKind && !gameManager.IsComboSelected(2) &&
                        gameManager.IsComboSelected(0) &&
                        gameManager.IsComboSelected(1) &&
                        gameManager.IsComboSelected(3) &&
                        gameManager.IsComboSelected(4) &&
                        gameManager.IsComboSelected(5)) ||

                    (fullHouse && !gameManager.IsComboSelected(3) &&
                        gameManager.IsComboSelected(0) &&
                        gameManager.IsComboSelected(1) &&
                        gameManager.IsComboSelected(2) &&
                        gameManager.IsComboSelected(4) &&
                        gameManager.IsComboSelected(5)) ||

                    ((smallStraight && !gameManager.IsComboSelected(4) &&
                        gameManager.IsComboSelected(0) &&
                        gameManager.IsComboSelected(1) &&
                        gameManager.IsComboSelected(2) &&
                        gameManager.IsComboSelected(3) &&
                        gameManager.IsComboSelected(5))|| (smallStraight && !gameManager.IsComboSelected(4) &&
                        gameManager.IsComboSelected(5))) ||

                    (largeStraight && !gameManager.IsComboSelected(5) &&
                        gameManager.IsComboSelected(0) &&
                        gameManager.IsComboSelected(1) &&
                        gameManager.IsComboSelected(2) &&
                        gameManager.IsComboSelected(3) &&
                        gameManager.IsComboSelected(4))
                )
                {
                    currentState = AIStates.RollDice1;
                    aiButton.GetComponentInChildren<TMP_Text>().text = "First Roll";
                }
                break;

            case AIStates.KeepDice:
                currentState = AIStates.RollDice2;
                if ((!gameManager.IsComboSelected(0) || !gameManager.IsComboSelected(1) || !gameManager.IsComboSelected(2) || !gameManager.IsComboSelected(3)) && (!smallStraight && !largeStraight))
                {
                    keepPairs();
                }

                else if ((!gameManager.IsComboSelected(4) || !gameManager.IsComboSelected(5)))
                {
                    CheckForConsecutiveNumbers();
                }

                aiButton.GetComponentInChildren<TMP_Text>().text = "Second Roll";
                currentState = AIStates.RollDice2;
                break;

            case AIStates.RollDice2:
                for (int i = 0; i < 6; i++)
                {
                    gameManager.SetComboActive(i, false);
                }
                gameManager.RollDice();
                currentState = AIStates.EvaluateDice2;
                aiButton.GetComponentInChildren<TMP_Text>().text = "Eval Combos";
                break;

            case AIStates.EvaluateDice2:
                gameManager.GetDiceValues(ref diceValues);
                CheckCombos();
                aiButton.GetComponentInChildren<TMP_Text>().text = "First Roll";
                currentState = AIStates.RollDice1;
                break;
            default:
                break;
        }
    }

    void CheckCombos()
    {
        twoPair = false;
        threeOfAKind = false;
        fourOfAKind = false;
        fullHouse = false;
        smallStraight = false;
        largeStraight = false;
        int pairCount = 0;
        for (int i = 0; i < frequency.Length; i++)
        {
            frequency[i] = 0;
        }
        foreach (int value in diceValues)
        {
            frequency[value]++;
        }

        for (int i = 0; i < frequency.Length; i++)
        {
            if (frequency[i] == 2 || frequency[i] == 3) //checking for SinglePairs
            {
                pairCount++;
            }

            if (frequency[i] == 3) //three of a kind
            {
                threeOfAKind = true;
                if (!gameManager.IsComboSelected(1))
                {
                    gameManager.SetComboActive(1, true);
                }
            }

            if (frequency[i] >= 4)
            {
                threeOfAKind = true;
                fourOfAKind = true;
                if (!gameManager.IsComboSelected(1))
                {
                    gameManager.SetComboActive(1, threeOfAKind);
                }
                if (!gameManager.IsComboSelected(2))
                {
                    gameManager.SetComboActive(2, fourOfAKind);
                }
            }
            if ((frequency[0] >= 1 && frequency[1] >= 1 && frequency[2] >= 1 && frequency[3] >= 1) || (frequency[2] >= 1 && frequency[3] >= 1 && frequency[4] >= 1 && frequency[5] >= 1) || (frequency[1] >= 1 && frequency[2] >= 1 && frequency[3] >= 1 && frequency[4] >= 1)) //small straight
            {
                smallStraight = true;
                if (!gameManager.IsComboSelected(4))
                {
                    gameManager.SetComboActive(4, smallStraight);
                }
            }
            if ((frequency[0] == 1 && frequency[1] == 1 && frequency[2] == 1 && frequency[3] == 1 && frequency[4] == 1) || (frequency[1] == 1 && frequency[2] == 1 && frequency[3] == 1 && frequency[4] == 1 && frequency[5] == 1)) //large straight
            {
                smallStraight = true;
                largeStraight = true;
                if (!gameManager.IsComboSelected(4))
                {
                    gameManager.SetComboActive(4, smallStraight);
                }
                if (!gameManager.IsComboSelected(5))
                {
                    gameManager.SetComboActive(5, largeStraight);
                }
            }
        }
        if (pairCount >= 2)
        {
            twoPair = true;
            if (!gameManager.IsComboSelected(0))
            {
                gameManager.SetComboActive(0, twoPair);
            }
        }
        if (pairCount == 2 && threeOfAKind)
        {
            fullHouse = true;
            twoPair = true;
            threeOfAKind = true;
            if (!gameManager.IsComboSelected(3))
            {
                gameManager.SetComboActive(3, fullHouse);
            }
            if (!gameManager.IsComboSelected(0))
            {
                gameManager.SetComboActive(0, twoPair);
            }
            if (!gameManager.IsComboSelected(1))
            {
                gameManager.SetComboActive(1, threeOfAKind);
            }
        }
    }

    void UpdateComboButtons()
    {
        if (largeStraight && !gameManager.IsComboSelected(5))
        {
            gameManager.SelectCombo(5);
        }
        else if (smallStraight && !gameManager.IsComboSelected(4))
        {
            gameManager.SelectCombo(4);
        }
        else if (fullHouse && !gameManager.IsComboSelected(3))
        {
            gameManager.SelectCombo(3);
        }
        else if (fourOfAKind && !gameManager.IsComboSelected(2))
        {
            gameManager.SelectCombo(2);
        }
        else if (twoPair && !gameManager.IsComboSelected(0))
        {
            gameManager.SelectCombo(0);
        }
        else if (threeOfAKind && !gameManager.IsComboSelected(1))
        {
            gameManager.SelectCombo(1);
        }
    }

    void onClick()
    {
        if (!gameManager.IsRolling())
        {
            TakeAIStep();
        }
    }

    void CheckForConsecutiveNumbers()
    {
        bool checked1 = false;
        bool checked2 = false;
        bool checked3 = false;
        bool checked4 = false;
        bool checked5 = false;
        bool checked6 = false;
     
        foreach (int value in diceValues)
        {
            if (value == 0 && frequency[1] >= 1 && frequency[2] >= 1)
            {
                for (int i = 0; i < diceValues.Length; i++)
                {                 
                    if (diceValues[i] == value && !checked1)
                    {
                        checked1 = true;
                        gameManager.KeepDie(i);   
                    }
                }
            }
            if (value == 1 && ((frequency[0] >= 1 && frequency[2] >= 1)|| (frequency[2] >=1 && frequency[3] >=1)))
            {        
                for (int i = 0; i < diceValues.Length; i++)
                {
                    if (diceValues[i] == value && !checked2)
                    {
                        checked2 = true;
                        gameManager.KeepDie(i);                      
                    }
                }
            }
            if (value == 2 && ((frequency[0] >= 1 && frequency[1] >= 1) || (frequency[1] >=1 && frequency[3] >=1) ||(frequency[3] >= 1 && frequency[4] >= 1)))
            {
                
                for (int i = 0; i < diceValues.Length; i++)
                {
                    if (diceValues[i] == value && !checked3)
                    {
                        checked3 = true;
                        gameManager.KeepDie(i);                     
                    }
                }
            }
            if (value == 3 && ((frequency[1] >= 1 && frequency[2] >= 1) || (frequency[2] >=1 && frequency[4] >=1) || (frequency[4] >= 1 && frequency[5] >= 1)))
            {
                
                for (int i = 0; i < diceValues.Length; i++)
                {
                    if (diceValues[i] == value && !checked4)
                    {
                        checked4 = true;
                        gameManager.KeepDie(i);                    
                    }
                }
            }
            if (value == 4 && ((frequency[2] >= 1 && frequency[3] >= 1) || (frequency[3] >= 1 && frequency[5] >= 1)))
            {
                
                for (int i = 0; i < diceValues.Length; i++)
                {
                    if (diceValues[i] == value && !checked5)
                    {
                        checked5 = true;
                        gameManager.KeepDie(i);                    
                    }
                }
            }
            if (value == 5 && frequency[3] >= 1 && frequency[4] >= 1)
            {
                
                for (int i = 0; i < diceValues.Length; i++)
                {
                    if (diceValues[i] == value && !checked6)
                    {
                        checked6 = true;
                        gameManager.KeepDie(i);                      
                    }
                }
            }
        }
    }

    void keepPairs()
    {
        if (((!gameManager.IsComboSelected(2) && gameManager.IsComboSelected(0) && gameManager.IsComboSelected(1) && gameManager.IsComboSelected(3)) && !fourOfAKind) || (gameManager.IsComboSelected(0) && gameManager.IsComboSelected(1) && !gameManager.IsComboSelected(2) && gameManager.IsComboSelected(3) && !fourOfAKind)) //if only 4 of a kind is left, keep maximum of 3 dice of same time or four of kind is not selected and every other pairing combo is selected, select maximum of 3 or 2 dice of same type
        {
            int diceToBeKept = 3;
            bool threeFrequency = false;
            for (int i = 0; i < frequency.Length; i++)
            {
                if (frequency[i] >= 3)
                {
                    threeFrequency = true;
                }
            }
            if (threeFrequency)
            {
                for (int i = 0; i < frequency.Length; i++)
                {
                    if (frequency[i] >= 3)
                    {
                        for (int j = 0; j < diceValues.Length; j++)
                        {
                            if (diceValues[j] == i)
                            {
                                if (diceToBeKept == 0)
                                {
                                    break;
                                }
                                gameManager.KeepDie(j);
                                diceToBeKept--;                               
                            }
                        }
                    }
                }
            }
            else
            {
                diceToBeKept = 2;
                for (int i = 0; i < frequency.Length; i++)
                {
                    if (frequency[i] == 2)
                    {
                        for (int j = 0; j < diceValues.Length; j++)
                        {
                            if (diceValues[j] == i)
                            {
                                if (diceToBeKept == 0)
                                {
                                    break;
                                }
                                gameManager.KeepDie(j);
                                diceToBeKept--;                             
                            }
                        }
                    }
                }
            }
        }

        else if(gameManager.IsComboSelected(3) && fullHouse && (!gameManager.IsComboSelected(0) || !gameManager.IsComboSelected(1) || !gameManager.IsComboSelected(2))) //if fullhouse is already seclected and we get a full house, keep 3 dice of same type
        {
            int diceToBeKept = 3;
            for (int i = 0; i < frequency.Length; i++)
            {
                if (frequency[i] >= 3)
                {
                    for (int j = 0; j < diceValues.Length; j++)
                    {
                        if (diceValues[j] == i)
                        {
                            if (diceToBeKept == 0)
                            {
                                break;
                            }
                            gameManager.KeepDie(j);
                            diceToBeKept--;                
                        }
                    }
                }
            }
        }

        else if (!gameManager.IsComboSelected(0) && !twoPair && (gameManager.IsComboSelected(1) && gameManager.IsComboSelected(2) && gameManager.IsComboSelected(3))) //if only two pair is left, keep maximum of 2 dice  of same type
        {
            int diceToBeKept = 2;
            for (int i = 0; i < frequency.Length; i++)
            {
                if (frequency[i] >= 2)
                {
                    for (int j = 0; j < diceValues.Length; j++)
                    {
                        if (diceValues[j] == i)
                        {
                            if (diceToBeKept == 0)
                            {
                                break;
                            }
                            gameManager.KeepDie(j);
                            diceToBeKept--;                         
                        }
                    }
                }
            }
        }

        else
        {
            for (int i = 0; i < frequency.Length; i++)
            {
                if (frequency[i] == 2 || frequency[i] == 3 || frequency[i] == 4)
                {
                    for (int j = 0; j < diceValues.Length; j++)
                    {
                        if (diceValues[j] == i)
                        {
                            gameManager.KeepDie(j);
                        }
                    }
                }
            }
        }
    }
}
