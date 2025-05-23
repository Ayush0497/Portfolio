using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadArea : MonoBehaviour
{
    [SerializeField] string area;

    private void OnTriggerEnter(Collider other)
    { 
        if (other.tag == "Player")

        SceneManager.LoadSceneAsync(area, LoadSceneMode.Additive);

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.UnloadSceneAsync(area);
        }
    }
}
