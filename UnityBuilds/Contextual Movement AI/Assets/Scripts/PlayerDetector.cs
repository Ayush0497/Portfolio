using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleLayer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (IsPlayerVisible(other.transform))
            {
                SendMessageUpwards("MoveTowards", other.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (IsPlayerVisible(other.transform))
            {
                SendMessageUpwards("MoveTowards", other.gameObject);
            }
        }
    }

    private bool IsPlayerVisible(Transform player)

    {

        Vector3 directionToPlayer = player.position - transform.position;

        Ray ray = new Ray(transform.position, directionToPlayer);

        // Perform the raycast to check if anything is between the Runner and Hunter

        if (Physics.Raycast(ray, out RaycastHit hitInfo, directionToPlayer.magnitude, obstacleLayer))
        {

            return false;
        }
        return true;
    }
}
