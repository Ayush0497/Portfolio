using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameData MyGameData;

    [SerializeField] List<string> _wordList;

    public bool CoopEnabled;

    [SerializeField]
    private Toggle _toggle;

    public Timer Timer;

    private void Start()
    {
        MyGameData = new GameData();
    }

    public void GetGameWord()
    {
        string word;
        do
        {
            word = GetWord();
        } while (word == MyGameData.GetGameWord());

        MyGameData.SetGameWord(word);
    }

    public string GetWord()
    {
        string randomWord = _wordList[Random.Range(0, _wordList.Count)];
        return randomWord;
    }

    public void ToggleCoop()
    {
        if (CoopEnabled)
        {
            CoopEnabled = !CoopEnabled;
        }
        else
        {
            CoopEnabled = true;
        }
    }

    /// <summary>
	/// Resets the score.
    /// And updates the UI to reflect this change.
	/// </summary>
    public void Reset()
    {
        _toggle.isOn = false;
        CoopEnabled = false;
        MyGameData = new GameData();
    }
}
