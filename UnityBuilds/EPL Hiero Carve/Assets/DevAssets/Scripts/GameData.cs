using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int Score;
    public int LettersComplete;
    public int WordsComplete;
    public string GameWord;
    public List<string> ShapesComplete;

    public GameData()
    {
        Score = 0;
        LettersComplete = 0;
        WordsComplete = 0;
        GameWord = string.Empty;
        ShapesComplete = new List<string>();
    }

    public int GetScore()
    {
        return Score;
    }

    public void SetScore(int changedScore)
    {
        Score = changedScore;
    }

    public int GetLettersComplete()
    {
        return LettersComplete;
    }

    public void SetLettersComplete(int newCount)
    {
        LettersComplete = newCount;
    }

    public int GetWordsComplete()
    {
        return WordsComplete;
    }

    public void SetWordsComplete(int newCount)
    {
        WordsComplete = newCount;
    }

    public string GetGameWord()
    {
        return GameWord;
    }

    public void SetGameWord(string word)
    {
        GameWord = word;
    }

    public List<string> GetShapesComplete()
    {
        return ShapesComplete;
    }

    public void SetShapesComplete(List<string> newList)
    {
        ShapesComplete = newList;
    }
}
