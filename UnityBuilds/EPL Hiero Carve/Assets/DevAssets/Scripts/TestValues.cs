using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestValues : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;

    public void AddShapes()
    {
        List<string> shapes = new List<string>();
        shapes.Add("A");
        shapes.Add("D");
        shapes.Add("Q");
        shapes.Add("R");
        _gameManager.MyGameData.SetShapesComplete(shapes);
    }

    public void AddLetter()
    {
        _gameManager.MyGameData.SetLettersComplete(_gameManager.MyGameData.GetLettersComplete() + 1);
    }

    public void AddWord()
    {
        _gameManager.MyGameData.SetWordsComplete(_gameManager.MyGameData.GetWordsComplete() + 1);
    }
}
