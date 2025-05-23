using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Endless : MonoBehaviour
{
    [SerializeField] private GameObject[] gameLevels; // Array of game levels
    [SerializeField] private GameObject startPosition; // Starting position for levels
    [SerializeField] private GameObject[] endPositions; // End positions for levels

    [SerializeField] private int currentLevel; // Current active level index

    private float sectionWidth = 70; // Width of each section
    private Vector3 levelOffset; // Offset for positioning levels

    private float StartPositionX; // X position of the start position
    private float EndPositionX; // X position of the current level's end position

    private void Start()
    {
        currentLevel = 0; // Initialize current level
        InitializeLevel(); // Initialize the level positions

        // Disable all levels except the first one
        for (int i = 0; i < gameLevels.Length; i++)
        {
            if (i != currentLevel)
            {
                gameLevels[i].SetActive(false);
                Debug.Log($"Map section {i} disabled."); // Log when a section is disabled
            }
            else
            {
                Debug.Log($"Map section {i} is active."); // Log when the first section is active
            }
        }
    }

    private void Update()
    {
        float progress = Mathf.InverseLerp(EndPositionX, StartPositionX, endPositions[currentLevel].transform.position.x);

        if (progress >= 1.0f)
        {
            TransitionToNextLevel();
        }
    }

    private void TransitionToNextLevel()
    {
        StartCoroutine(ChangeLevelTransform());
    }

    private IEnumerator ChangeLevelTransform()
    {
        // Log the current level before changing
        Debug.Log($"Transitioning from map section {currentLevel}.");

        // Randomly select the next level, ensuring it's different from the current one
        int nextLevel;
        do
        {
            nextLevel = Random.Range(0, gameLevels.Length);
        } while (nextLevel == currentLevel); // Ensure it's not the same as currentLevel

        // Set the new level
        currentLevel = nextLevel;

        // Calculate the level offset based on the section width
        levelOffset = new Vector3(sectionWidth / 2, 0, 14.15621f);

        // Position the new level
        gameLevels[currentLevel].transform.position = new Vector3(
            startPosition.transform.position.x + levelOffset.x,
            0,
            startPosition.transform.position.z + levelOffset.z
        );

        gameLevels[currentLevel].SetActive(true); // Activate the new level
        Debug.Log($"Map section {currentLevel} positioned at {gameLevels[currentLevel].transform.position}."); // Log new position

        InitializeLevel();

        // Wait for a specific duration without disabling any levels
        yield return new WaitForSeconds(15f);
    }

    private void InitializeLevel()
    {
        StartPositionX = startPosition.transform.position.x;
        EndPositionX = endPositions[currentLevel].transform.position.x;
    }
}