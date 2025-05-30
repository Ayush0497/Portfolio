using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int value;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            MyEvents.updateScore.Invoke(value);
            Destroy(gameObject);
        }
    }
}
