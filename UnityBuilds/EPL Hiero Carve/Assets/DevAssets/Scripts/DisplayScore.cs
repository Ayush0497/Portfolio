using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;
    private void Update()
    {
        if (GetComponent<TextMeshProUGUI>().text != _gameManager.MyGameData.GetScore().ToString())
        {
            Debug.Log(_gameManager.MyGameData.GetScore());
            GetComponent<TextMeshProUGUI>().text = _gameManager.MyGameData.GetScore().ToString();
        }
    }
}
