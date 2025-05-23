using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.SocialPlatforms.Impl;
public class Spawner : MonoBehaviour
{
    public SaveContainer myContainer;
    public GhostDataContainer previousGhostDataContainer;
    //public GhostDataContainer newGhostDataContainer;

    public Material[] myColors;
    public GameObject[] myVehicles;
    public GameObject GhostVehicle;
    public Movement currentVehicle;
    Vector3 movePos;
    Quaternion moveRot;

    // Use this for initialization
    void Start()
    {
        LoadData();
        //Spawning the Vehicle
        int playerVehicle = myContainer.players[myContainer.currentIndex].GetShape();
        int playerColor = myContainer.players[myContainer.currentIndex].GetColor();
        myVehicles[playerVehicle].transform.position = gameObject.transform.position;
        myVehicles[playerVehicle].transform.rotation = gameObject.transform.rotation;

        for (int i = 0; i < myVehicles.Length; i++)
        {
            if (i != playerVehicle)
            {
                myVehicles[i].SetActive(false);
            }
        }
        myVehicles[playerVehicle].GetComponent<MeshRenderer>().sharedMaterial.color = myColors[playerColor].color;

        //Spawning the Ghost
        previousGhostDataContainer = myContainer.players[myContainer.currentIndex].GetGhostData();

        currentVehicle = myVehicles[playerVehicle].GetComponent<Movement>();
        
    }

    private void FixedUpdate()
    {
        if (previousGhostDataContainer != null)
        {
            if (previousGhostDataContainer.GetNextPosition(ref movePos))
            {
                GhostVehicle.transform.position = movePos;
                GhostVehicle.transform.rotation = moveRot;
            }
        }

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
        }
    }

    public void SaveScore(float changeScore)
    {
        if (changeScore < myContainer.players[myContainer.currentIndex].GetScore())
        {
            myContainer.players[myContainer.currentIndex].SetScore(changeScore);
        }

        CheckTopScores(changeScore, myContainer.players[myContainer.currentIndex].GetName());

        Stream stream = File.Open("SaveFiles/Profiles.xml", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(SaveContainer));
        serializer.Serialize(stream, myContainer);
        stream.Close();
    }

    public void SaveGhostData(GhostDataContainer ghostData)
    {
        myContainer.players[myContainer.currentIndex].SetGhostData(ghostData);

        Stream stream = File.Open("SaveFiles/Profiles.xml", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(SaveContainer));
        serializer.Serialize(stream, myContainer);
        stream.Close();
    }

    void CheckTopScores(float checkScore, string checkName)
    {
        for (int i = 0; i < myContainer.leaders.Length; i++)
        {

            // Check if the new score qualifies for this position (lower score is better)
            if (checkScore < myContainer.leaders[i].GetScore())
            {
                // Store the existing score and name temporarily
                float tempScore = myContainer.leaders[i].GetScore();
                string tempName = myContainer.leaders[i].GetName();

                // Insert the new score and name at the current position
                myContainer.leaders[i].SetScore(checkScore);
                myContainer.leaders[i].SetName(checkName);

                // Start shifting down subsequent scores
                for (int j = i + 1; j < myContainer.leaders.Length; j++)
                {
                    // Move the score and name from the next position to the current one
                    float nextTempScore = myContainer.leaders[j].GetScore();
                    string nextTempName = myContainer.leaders[j].GetName();

                    // Set the current position to the previous score and name
                    myContainer.leaders[j].SetScore(tempScore);
                    myContainer.leaders[j].SetName(tempName);

                    // Prepare for the next iteration
                    tempScore = nextTempScore;
                    tempName = nextTempName;
                }

                // After shifting down, exit the method since we've inserted the new score
                break; // Exit the loop after inserting the new score
            }
        }
    }

    public void FinishGameWithOutGhostData()
    {
        SaveScore(currentVehicle.score);
        SceneManager.LoadScene(0);

    }

    public void FinishGameWithGhostData()
    {
        SaveScore(currentVehicle.score);
        SaveGhostData(currentVehicle.GhostDataContainer);
        SceneManager.LoadScene(0);
    }
}
