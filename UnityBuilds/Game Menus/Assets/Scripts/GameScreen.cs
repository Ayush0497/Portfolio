using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameScreen : MonoBehaviour
{
    [SerializeField] GameObject MainGamePanel;
    [SerializeField] GameObject PauseMenuPanel;
    [SerializeField] TextMeshProUGUI details;
    [SerializeField] Image Image;

    private void Start()
    {
        if (string.IsNullOrWhiteSpace(CharacterDetails.PlayerStats.characterName))
        {
            CharacterDetails.PlayerStats.characterName = "No Name";
        }
        PauseMenuPanel.SetActive(false);
        details.text = "Character Name: " + CharacterDetails.PlayerStats.characterName + "\n" + "HP: " + CharacterDetails.PlayerStats.hp + "\n" + "Damage: " + CharacterDetails.PlayerStats.damage + "\n" + "Accuracy: " + CharacterDetails.PlayerStats.accuracy + "\n" + "Unused Points: " + CharacterDetails.PlayerStats.skillPointsAvaialble;
        Image.sprite = CharacterDetails.PlayerStats.characterImage.sprite;
    }

    private void Update()
    {  
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainGamePanel.SetActive(false);
            PauseMenuPanel.SetActive(true);
        }
    }

    public void onResume()
    {
        MainGamePanel.SetActive(true);
        PauseMenuPanel.SetActive(false);
    }
}
