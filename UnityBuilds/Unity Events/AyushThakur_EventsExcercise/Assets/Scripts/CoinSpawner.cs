using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] GameObject EndScreen;
    [SerializeField] GameObject StartScreen;

    void Start()
    {
        MyEvents.spawnCoins.Invoke();
        StartCoroutine(spawn());
    }

    private IEnumerator spawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(2.0f);
            MyEvents.spawnCoins.Invoke();
        }
    }
}
