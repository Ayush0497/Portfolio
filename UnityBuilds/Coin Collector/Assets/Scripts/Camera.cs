using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed;
    public Vector3 offSet;

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPostion = target.position + offSet;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPostion, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
