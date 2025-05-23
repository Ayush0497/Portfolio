using UnityEngine;

public class ChangeScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject _currentScreen;
    [SerializeField]
    private GameObject _nextScreen;
    [SerializeField]
    private LoadShape _loadShape;

    /// <summary>
	/// Deactivate the current panel.
    /// Activate the next panel.
	/// </summary>
    public void ChangeTheScreen()
    {
        if (_currentScreen.activeSelf)
        {
            _currentScreen.SetActive(false);
        }
        if (!_nextScreen.activeSelf)
        {
            _nextScreen.SetActive(true);
        }
        if(_loadShape!=null)
        {
            _loadShape.ResetGame();
        } 
    }
}
