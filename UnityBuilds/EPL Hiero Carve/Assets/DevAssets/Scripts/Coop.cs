using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coop : MonoBehaviour
{
    [SerializeField]
    private GameManager _leftManager, _centerManager, _rightManager;
    public GameData CoopData;
    public float Time;

    public float TimeOut;
    public Timer TrackTimer;

    private void Start()
    {
        CoopData = new GameData();
    }

    public void Update()
    {
        GetCombinedScore();
    }

    public void GetCombinedScore()
    {
        int totalWords = 0;
        int totalLetters = 0;
        List<string> glyphsComplete = new List<string>();

        if (_leftManager.CoopEnabled)
        {
            totalWords += _leftManager.MyGameData.GetWordsComplete();
            totalLetters += _leftManager.MyGameData.GetLettersComplete();
            glyphsComplete.AddRange(_leftManager.MyGameData.GetShapesComplete());
        }
        if (_centerManager.CoopEnabled)
        {
            totalWords += _centerManager.MyGameData.GetWordsComplete();
            totalLetters += _centerManager.MyGameData.GetLettersComplete();
            glyphsComplete.AddRange(_centerManager.MyGameData.GetShapesComplete());
        }
        if (_rightManager.CoopEnabled)
        {
            totalWords += _rightManager.MyGameData.GetWordsComplete();
            totalLetters += _rightManager.MyGameData.GetLettersComplete();
            glyphsComplete.AddRange(_rightManager.MyGameData.GetShapesComplete());
        }

        CoopData.SetScore((totalWords * 60) + (totalLetters * 10));
        CoopData.SetWordsComplete(totalWords);
        CoopData.SetLettersComplete(totalLetters);
        CoopData.SetShapesComplete(glyphsComplete);
    }
}