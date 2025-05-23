using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class BucketMovement : MonoBehaviour
{
    [SerializeField] InputAction bucketMovementAction;
    [SerializeField] Vector2 bucketMovement;
    [SerializeField] float bucketRotationSpeed = 90.0f;
    [SerializeField] GameObject bucket;
    Vector3 armsAngle;
    Vector3 bucketAngle;
    void Update()
    {
        bucketMovement = bucketMovementAction.ReadValue<Vector2>();
        transform.Rotate(Vector3.left, bucketRotationSpeed * Time.deltaTime * bucketMovement.y);
        bucket.transform.Rotate(Vector3.right, bucketRotationSpeed * Time.deltaTime * bucketMovement.x);
        armsAngle = transform.localRotation.eulerAngles;
        bucketAngle = bucket.transform.localRotation.eulerAngles;
        if (armsAngle.x > 25 && armsAngle.x < 180)
        {
            transform.localRotation = Quaternion.Euler(25.0f, 0, 0);
        }
        if (armsAngle.x > 180 && armsAngle.x < 300)
        {
            transform.localRotation = Quaternion.Euler(300.0f, 0, 0);
        }

        if (bucketAngle.x > 70 && bucketAngle.x < 180)
        {
            bucket.transform.localRotation = Quaternion.Euler(70.0f, 0, 0);
        }

        if (bucketAngle.x > 180 && bucketAngle.x < 320)
        {
            bucket.transform.localRotation = Quaternion.Euler(320.0f, 0, 0);
        }
    }

    void OnEnable()
    {
        bucketMovementAction.Enable();
    }

    void OnDisable()
    {
        bucketMovementAction.Disable();
    }
}
