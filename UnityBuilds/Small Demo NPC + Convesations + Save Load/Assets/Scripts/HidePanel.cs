using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePanel : MonoBehaviour
{
    private void Update()
    {
        bool shouldBeActive = PlayerInfo.piInstance.hasMetNPC;
        if(shouldBeActive)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 1.0f;
        }
        else
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0.0f;
        }
    }
}
