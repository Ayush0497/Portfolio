using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guards : MonoBehaviour
{
    [SerializeField] GuardPathFinder guard;
    [SerializeField] private LayerMask obstacleLayer;
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "blue")
        {
            if (IsPlayerVisible(collision.transform))
            {
                guard.mystate = GuardPathFinder.state.CapturedSomeone;
                Events.CapturedBlueSpy.Invoke();
                StartCoroutine(ResetGuard());
            }
            
        }
        if (collision.gameObject.tag == "red")
        {
            if (IsPlayerVisible(collision.transform))
            {
                guard.mystate = GuardPathFinder.state.CapturedSomeone;
                Events.CapturedRedSpy.Invoke();
                StartCoroutine(ResetGuard());
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

    IEnumerator ResetGuard()
    {
        yield return new WaitForSeconds(1f);
        guard.mystate = GuardPathFinder.state.Patrolling;
    }
}
