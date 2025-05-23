using TMPro;
using UnityEngine;

public class LoadEndValues : MonoBehaviour
{
    [SerializeReference]
    private GameObject[] _glyphs;
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private TextMeshProUGUI _score, _letters, _words;

    private void OnEnable()
    {
        LoadShapesCompleted();
        LoadScores();
    }

    private void LoadShapesCompleted()
    {
        if (_gameManager.CoopEnabled)
        {
            if (GameObject.FindGameObjectWithTag("Coop") != null)
            {
                GameData myCoopData = GameObject.FindGameObjectWithTag("Coop").GetComponent<Coop>().CoopData;

                foreach (GameObject glyph in _glyphs)
                {
                    if (myCoopData.GetShapesComplete().Contains(glyph.name))
                    {
                        glyph.SetActive(true);
                    }
                    else
                    {
                        glyph.SetActive(false);
                    }
                }
            }
        }
        else
        {
            foreach (GameObject glyph in _glyphs)
            {
                if (_gameManager.MyGameData.GetShapesComplete().Contains(glyph.name))
                {
                    glyph.SetActive(true);
                }
                else
                {
                    glyph.SetActive(false);
                }
            }
        }
    }

    private void LoadScores()
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
            _gameManager.MyGameData.SetScore((_gameManager.MyGameData.GetWordsComplete() * 60) + (_gameManager.MyGameData.GetLettersComplete() * 10));

            _score.text = "Score: " + _gameManager.MyGameData.GetScore();
            _letters.text = "Letters Completed: " + _gameManager.MyGameData.GetLettersComplete();
            _words.text = "Words Completed: " + _gameManager.MyGameData.GetWordsComplete();
        }
    }

    private void OnDisable()
    {
        if (GameObject.FindGameObjectWithTag("Coop") != null && _gameManager.CoopEnabled)
        {
            GameObject.FindGameObjectWithTag("Coop").GetComponent<Coop>().CoopData = new GameData();
            GameObject.FindGameObjectWithTag("Coop").GetComponent<Coop>().TimeOut = 60;
            GameObject.FindGameObjectWithTag("Coop").GetComponent<Coop>().Time = 300;
        }

        _score.text = "Score: 0";
        _letters.text = "Letters Complete: 0";
        _words.text = "Words Complete: 0";
    }
}