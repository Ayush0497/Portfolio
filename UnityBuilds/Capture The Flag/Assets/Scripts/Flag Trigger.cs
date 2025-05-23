using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagTrigger : MonoBehaviour
{
    [SerializeField] Mmovement Player1;
    [SerializeField] Mmovement Player2;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player1")
        {
            Player1.FlagInteraction();
            gameObject.SetActive(false);
        }
        if(other.gameObject.tag == "Player2")
        {
            Player2.FlagInteraction();
            gameObject.SetActive(false);
        }
    }
}
