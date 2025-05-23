using UnityEngine;
/// <summary>
/// IMPORTANT!
/// This script appears to be redundant.
/// </summary>
public class PanelSwitching : MonoBehaviour
{
    [SerializeField] GameObject[] Panels;

    private void Start()
    {
        foreach (GameObject panel in Panels)
        {
            if (panel.gameObject.tag != "TitleScreenLeft" && panel.gameObject.tag != "TitleScreenCenter" && panel.gameObject.tag != "TitleScreenRight")
            {
                panel.SetActive(false);
            }
        }
    }

    public void LoadPanels(string PanelToLoad)
    {
        foreach (GameObject panel in Panels)
        {
            if (panel.gameObject.tag == PanelToLoad)
            {
                panel.SetActive(true);
            }
        }
    }
    public void ClosePanels(string PanelToClose)
    {
        foreach (GameObject panel in Panels)
        {
            if (panel.gameObject.tag == PanelToClose)
            {
                panel.SetActive(false);
            }
        }
    }
}
