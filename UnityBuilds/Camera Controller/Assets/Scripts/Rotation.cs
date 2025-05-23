using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    Vector3 rotation;
    bool rotateToOtherSide = false;
    private void LateUpdate()
    {
        if (rotateToOtherSide)
        {
            transform.Rotate(0.0f, 0.03f, 0.0f);
        }
        else
        {
            transform.Rotate(0.0f, -0.03f, 0.0f);
        }
    }

    private void Update()
    {
        rotation = transform.localRotation.eulerAngles;
        if (rotation.y < 25.0f)
        {
            rotateToOtherSide = true;
            transform.localRotation = Quaternion.Euler(rotation.x, 25.0f, rotation.z);
        }
        if (rotation.y > 65.0f)
        {
            rotateToOtherSide = false;
            transform.localRotation = Quaternion.Euler(rotation.x, 65.0f, rotation.z);
        }
    }
}
