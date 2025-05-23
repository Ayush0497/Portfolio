using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDetails : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] TextMeshProUGUI accuracyText;
    [SerializeField] TextMeshProUGUI skillPointsAvailableText;
    [SerializeField] TMP_Dropdown myDropdown;
    [SerializeField] TMP_InputField input;
    [SerializeField] Image image;
    [SerializeField] Sprite[] characterSprites;
    private int currentIndex = 0;

    public static class PlayerStats
    {
        public static int hp;
        public static int damage;
        public static int accuracy;
        public static string characterName;
        public static int skillPointsAvaialble;
        public static Image characterImage;
    }


    private void Start()
    {
        PlayerStats.skillPointsAvaialble = 10;
        PlayerStats.hp = 80;
        PlayerStats.damage = 80;
        PlayerStats.accuracy = 80;
        PlayerStats.characterName = "";
        image.sprite = characterSprites[0];
        PlayerStats.characterImage = image;
        currentIndex = 0;
    }

    private void Update()
    {
        hpText.text = PlayerStats.hp.ToString();
        damageText.text = PlayerStats.damage.ToString();
        accuracyText.text = PlayerStats.accuracy.ToString();
        skillPointsAvailableText.text = "Points Available:" + PlayerStats.skillPointsAvaialble;
        PlayerStats.characterImage = image;
    }

    public void onDropDownValueChanged(int index)
    {
        if(index == 0)
        {
            PlayerStats.hp = 80;
            PlayerStats.damage = 80;
            PlayerStats.accuracy = 80;
            PlayerStats.skillPointsAvaialble = 10;
        }

        else if(index == 1)
        {
            PlayerStats.hp = 70;
            PlayerStats.damage = 100;
            PlayerStats.accuracy = 80;
            PlayerStats.skillPointsAvaialble = 10;
        }
        else if(index == 2)
        {
            PlayerStats.hp = 100;
            PlayerStats.damage = 80;
            PlayerStats.accuracy = 80;
            PlayerStats.skillPointsAvaialble = 10;
        }
    }

    public void increaseHP()
    {
        if (PlayerStats.hp!=100 && PlayerStats.skillPointsAvaialble!=0)
        {
            PlayerStats.hp++;
            PlayerStats.skillPointsAvaialble--;
        }
    }

    public void decreaseHP()
    {
        if (PlayerStats.hp != 50)
        {
            PlayerStats.hp--;
            PlayerStats.skillPointsAvaialble++;
        }
    }

    public void increaseDamage()
    {
        if (PlayerStats.damage !=100 && PlayerStats.skillPointsAvaialble != 0)
        {
            PlayerStats.damage++;
            PlayerStats.skillPointsAvaialble--;
        }
    }

    public void decreaseDamage()
    {
        if (PlayerStats.damage != 50)
        {
            PlayerStats.damage--;
            PlayerStats.skillPointsAvaialble++;
        }
    }

    public void increaseAccuracy()
    {
        if(PlayerStats.accuracy != 100 && PlayerStats.skillPointsAvaialble != 0)
        {
            PlayerStats.accuracy++;
            PlayerStats.skillPointsAvaialble--;
        }
    }

    public void decreaseAccuracy()
    {
        if (PlayerStats.accuracy != 50)
        {
            PlayerStats.accuracy--;
            PlayerStats.skillPointsAvaialble++;
        }
    }

    public void onInput()
    {
        PlayerStats.characterName = input.text;
    }

    public void changeNextImage()
    {
        if(currentIndex == 0)
        {
            image.sprite = characterSprites[1];
            currentIndex = 1;
        }
        else if(currentIndex == 1)
        {
            image.sprite = characterSprites[2];
            currentIndex = 2;
        }
        else if(currentIndex == 2)
        {
            image.sprite = characterSprites[0];
            currentIndex = 0;
        }
    }

    public void changePreviousImage()
    {
        if (currentIndex == 0)
        {
            image.sprite = characterSprites[2];
            currentIndex = 2;
        }
        else if (currentIndex == 1)
        {
            image.sprite = characterSprites[0];
            currentIndex = 0;
        }
        else if (currentIndex == 2)
        {
            image.sprite = characterSprites[1];
            currentIndex = 1;
        }
    }
}
