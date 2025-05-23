using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualRotation : MonoBehaviour
{
    void Update()
    {
        Vector3 rotation;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0.0f, -0.03f, 0.0f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0.0f, 0.03f, 0.0f);
        }
        rotation = transform.localRotation.eulerAngles;
        if (rotation.y < 25.0f)
        {
            transform.localRotation = Quaternion.Euler(rotation.x, 25.0f, rotation.z);
        }
        if (rotation.y > 65.0f)
        {
            transform.localRotation = Quaternion.Euler(rotation.x, 65.0f, rotation.z);
        }
    }
}
