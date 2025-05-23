using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml.Serialization;
using TMPro;
using System.Collections.Generic;
using System;

public class DataManager : MonoBehaviour
{
    public SaveContainer myContainer;

    public Button[] profileButtons;
    public Button playButton;
    public Button deleteButton;
    public GameObject DeletePanel;

    public TextMeshProUGUI[] leaderText;

    public TMP_InputField nameField;
    public TMP_Dropdown colorDropdown;
    public TMP_Dropdown vehicleDropdown;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI confirmText;
    public int index;
    bool deleted;
    // Use this for initialization
    void Start()
    {
        myContainer = new SaveContainer();
        LoadData();
        deleted = false;
        index = -1;
        playButton.interactable = false;
        deleteButton.interactable = false;
        nameField.interactable = false;
        colorDropdown.interactable = false;
        vehicleDropdown.interactable = false;
        DeletePanel.SetActive(false);
    }

    public void LoadData()
    {
        // If the XML file exists then load the data.
        if (File.Exists("SaveFiles/Profiles.xml"))
        {
            Stream stream = File.Open("SaveFiles/Profiles.xml", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveContainer));
            myContainer = serializer.Deserialize(stream) as SaveContainer;
            stream.Close();
            Array.Sort(myContainer.leaders, (a, b) => a.GetScore().CompareTo(b.GetScore()));

            // Reset the index for the leaderText
            int j = 0;

            // Iterate through the sorted leaders
            for (int i = 0; i < myContainer.leaders.Length; i++)
            {
                // Only display leaders  that do not have the default score
                if (myContainer.leaders[i].GetScore() != 100000.0f)
                {
                    leaderText[j].text = (j + 1) + ": " + myContainer.leaders[i].GetName() + "  " + myContainer.leaders[i].GetScore().ToString("0.00");
                    j++;
                }
            }
        }

        UpdateProfileButtons();
    }

    public void SaveData()
    {
        if ((!Directory.Exists("SaveFiles")))
        {
            Directory.CreateDirectory("SaveFiles");
        }
        Stream stream = File.Open("SaveFiles/Profiles.xml", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(SaveContainer));
        serializer.Serialize(stream, myContainer);
        stream.Close();
    }

    public void SelectProfile(int buttonIndex)
    {
        // If the profile button pressed does not yet have a profile associated with it then add a new profile.
        if (buttonIndex > myContainer.players.Count - 1)
        {
            // Add a profile and set the index to that profile.
            myContainer.AddProfile();
            index = myContainer.players.Count - 1;
            myContainer.currentIndex = index;
            UpdateProfileButtons();
        }
        // Otherwise just select the profile
        else
        {
            // Set the index to the profile selected and update the profile info.
            index = buttonIndex;
            myContainer.currentIndex = index;
            UpdateProfileButtons();
        }
        nameField.text = myContainer.players[index].GetName();
        colorDropdown.value = myContainer.players[index].GetColor();
        vehicleDropdown.value = myContainer.players[index].GetShape();
        if(myContainer.players[index].GetScore() == 100000.0f)
        {
            scoreText.text = "Highscore: None";
        }
        else
        {
            scoreText.text = "Highscore: " + myContainer.players[index].GetScore().ToString("0.00");
        }
       
        playButton.interactable = true;
        deleteButton.interactable = true;
        UpdateProfileButtons();
    }

    public void DeleteProfile()
    {
        if (index < myContainer.players.Count)
        {
            // Remove the selected profile.
            myContainer.players.RemoveAt(index);
            playButton.interactable = false;
            deleteButton.interactable = false;
            nameField.interactable = false;
            colorDropdown.interactable = false;
            vehicleDropdown.interactable = false;
            deleted = true;
            DeletePanel.SetActive(false);
            UpdateProfileButtons();
        }
    }

    public void OpenDeletePanel()
    {
        DeletePanel.SetActive(true);
        confirmText.text = $"Are you sure you want to delete '{myContainer.players[index].name}' Profile?";
    }

    public void closeDeletePanel()
    {
        DeletePanel.SetActive(false);
    }

    void UpdateProfileButtons()
    {
        // Set all of the profile buttons active state to false.
        for (int i = 0; i < profileButtons.Length; i++)
        {
            profileButtons[i].gameObject.SetActive(false);
        }

        // For each loaded profile activate the profile button and change the text to the name of the profile.
        for (int i = 0; i < myContainer.players.Count; i++)
        {
            profileButtons[i].gameObject.SetActive(true);
            profileButtons[i].GetComponentInChildren<TMP_Text>().text = myContainer.players[i].GetName();
        }

        // If the number of profiles loaded is less than the profile buttons available then activate the next
        // profile button and set the text to "Add Profile".
        if (myContainer.players.Count < 3)
        {
            profileButtons[myContainer.players.Count].gameObject.SetActive(true);
            profileButtons[myContainer.players.Count].GetComponentInChildren<TMP_Text>().text = "Add Profile";
        }

        if (myContainer.players.Count == 0||deleted)
        {
            nameField.interactable = false;
            colorDropdown.interactable = false;
            vehicleDropdown.interactable = false;
            deleted = false;
        }
        else
        {
            nameField.interactable = true;
            colorDropdown.interactable = true;
            vehicleDropdown.interactable = true;
        }
        SaveData();
    }

    public void ChangeName(string changeName)
    {
        myContainer.players[index].SetName(changeName);
        UpdateProfileButtons();
    }
    public void ChangeColor(int changeColor)
    {
        myContainer.players[index].SetColor(changeColor);
        UpdateProfileButtons();
    }
    public void ChangeVechicle(int changeShape)
    {
        myContainer.players[index].SetVehicle(changeShape);
        UpdateProfileButtons();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ClearLeaderBoard()
    {
        for (int i = 0; i < myContainer.leaders.Length; i++)
        {
            myContainer.leaders[i].SetName("Empty");
            myContainer.leaders[i].SetScore(100000.0f);
            leaderText[i].text = $"{i+1}: None";
            SaveData();
        }
    }
}
