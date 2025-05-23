using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameScores : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI _score, _letters, _words;

    private void Update()
    {
        if (_gameManager.CoopEnabled)
        {
            if (GameObject.FindGameObjectWithTag("Coop") != null)
            {
                GameData myCoopData = GameObject.FindGameObjectWithTag("Coop").GetComponent<Coop>().CoopData;
                _score.text = $"Co-op Score: {myCoopData.GetScore()}";
                _letters.text = $"Letters Completed: {myCoopData.GetLettersComplete()}";
                _words.text = $"Words Completed: {myCoopData.GetWordsComplete()}";
            }
        }
        else
        {
            _score.text = $"Score: {(_gameManager.MyGameData.WordsComplete * 60) + (_gameManager.MyGameData.LettersComplete * 10)}";
            _letters.text = $"Letters Completed: {_gameManager.MyGameData.LettersComplete}";
            _words.text = $"Words Completed: {_gameManager.MyGameData.WordsComplete}";
        }
    }
}