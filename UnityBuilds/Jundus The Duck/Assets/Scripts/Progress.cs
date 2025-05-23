using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    [SerializeField] ProgressBar progressBar;
    [SerializeField] GameObject start;
    [SerializeField] GameObject end;

    private float StartPositionX;
    private float EndPositionX;

    private void Start()
    {
        progressBar.BarValue = 0;
        StartPositionX = start.transform.position.x;
        EndPositionX = end.transform.position.x;
    }

    private void Update()
    {
        float progress = Mathf.InverseLerp(EndPositionX, StartPositionX, end.transform.position.x);
        progressBar.BarValue = Mathf.RoundToInt(progress * 100f);
    }
}
