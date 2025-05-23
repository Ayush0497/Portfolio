using TMPro;
using UnityEngine;

public class Panels : MonoBehaviour
{
    GameObject first, second, third, fourth;

    void Start()
    {
        first = GameObject.FindGameObjectWithTag("first");
        second = GameObject.FindGameObjectWithTag("second");
        third = GameObject.FindGameObjectWithTag("third");
        fourth = GameObject.FindGameObjectWithTag("fourth");
        first.transform.localScale = new Vector3(1, 1, 1);
        second.transform.localScale = new Vector3(0, 0, 0);
        third.transform.localScale = new Vector3(0, 0, 0);
        fourth.transform.localScale = new Vector3(0,0, 0);
    }

    public void onStart()
    {
        first.transform.localScale = new Vector3(0, 0, 0);
        second.transform.localScale = new Vector3(1, 1, 1);
    }

    public void onPlay()
    {
        first.transform.localScale = new Vector3(0, 0, 0);
        second.transform.localScale = new Vector3(0, 0, 0);
        third.transform.localScale = new Vector3(1,1,1);
    }

    public void onBack()
    {
        first.transform.localScale = new Vector3(1, 1, 1);
        second.transform.localScale = new Vector3(0, 0, 0);
        third.transform.localScale = new Vector3(0, 0, 0);
    }

    public void onRestart()
    {
        first.transform.localScale = new Vector3(0, 0, 0);
        second.transform.localScale = new Vector3(1, 1, 1);
        third.transform.localScale = new Vector3(0, 0, 0);
        fourth.transform.localScale = new Vector3(0, 0, 0);
    }
}
