using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static DontDestroyOnLoad instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Another instance exists, destroy this one
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
