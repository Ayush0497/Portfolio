using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class ObjectDetector : MonoBehaviour

{

    [SerializeField] private LayerMask obstacleLayer;

    private void OnTriggerEnter(Collider other)

    {

        if (other.gameObject.tag == "PickUp")
        {

            SendMessageUpwards("MoveTowards", other.gameObject);

        }

        if (other.gameObject.tag == "Hunter")
        {

            if (IsHunterVisible(other.transform))

            {
                SendMessageUpwards("MoveAway", other.gameObject);

            }

        }
    }

    private bool IsHunterVisible(Transform hunter)

    {

        Vector3 directionToHunter = hunter.position - transform.position;

        Ray ray = new Ray(transform.position, directionToHunter);

        // Perform the raycast to check if anything is between the Runner and Hunter

        if (Physics.Raycast(ray, out RaycastHit hitInfo, directionToHunter.magnitude, obstacleLayer))

        {

            return false;

        }

        return true;

    }

}

