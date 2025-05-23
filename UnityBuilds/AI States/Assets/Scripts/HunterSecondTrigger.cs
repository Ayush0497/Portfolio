using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterSecondTrigger : MonoBehaviour
{ 
    [SerializeField] GameObject Cage;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject firstHunter;
    [SerializeField] GameObject SecondHunter;
    [SerializeField] AdvancedHunterAI Hunter;
    [SerializeField] GameObject wall;
    Rigidbody first;
    Rigidbody second;
    private void Start()
    {
        if (Cage != null)
        {
            Cage.SetActive(false);
        }
        if(wall != null)
        {
            wall.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(Hunter != null)
            {
                Hunter.mystate = AdvancedHunterAI.state.Captured;
                StartCoroutine(ChangeToPatrolling());
            }
            SecondHunter.gameObject.SetActive(false);
            wall.SetActive(true);
            Player.SetActive(false);
            Cage.SetActive(true);
        }
    }

    IEnumerator ChangeToPatrolling()
    {
        yield return new WaitForSeconds(3);
        Hunter.mystate = AdvancedHunterAI.state.Patrolling;
    }
}
