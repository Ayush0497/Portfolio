using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdvancedHunterAI : MonoBehaviour
{
    [SerializeField] public float movementSpeed;
    [SerializeField] Rigidbody rbody;
    [SerializeField] GameObject detector;
    //[SerializeField] float forceAmount = 10;
    [SerializeField] LayerMask myMask;
    [SerializeField] GameObject playerView;
    [SerializeField] bool leftCast, rightCast;
    [SerializeField] TextMeshProUGUI StateText;
    [SerializeField] TextMeshProUGUI previousStateText;
    private state previousState;
    RaycastHit hit;
    public enum state
    {
        Patrolling,
        ChasingEnemy,
        RequestedAdditionalGuards,
        EnemyEscapedSearching,
        Captured,
    }

    public state mystate = state.Patrolling;
    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (StateText != null && previousStateText !=null)
        {
            if (mystate != previousState)
            {
                previousStateText.text = $"Previous State: {previousState}";
                previousState = mystate;
            }

            if (mystate == state.Patrolling)
            {
                StateText.text = "Guard State: Patrolling";
            }
            else if (mystate == state.ChasingEnemy)
            {
                StateText.text = "Guard State: Chasing Enemy";
            }
            else if (mystate == state.RequestedAdditionalGuards)
            {
                StateText.text = "Guard State: Enemy Escaped, Requested Additional Guards";
            }
            else if (mystate == state.EnemyEscapedSearching)
            {
                StateText.text = "Guard State: Enemy Escaped, Searching with Second Guard";
            }
            else if (mystate == state.Captured)
            {
                StateText.text = "Guard State: Captured";
            }
            Vector3 currentRotation = rbody.rotation.eulerAngles;
            if (rbody.rotation.z != 0 || rbody.rotation.x != 0)
            {
                currentRotation.z = 0;
                currentRotation.x = 0;
                rbody.rotation = Quaternion.Euler(currentRotation);
            }
        }
        
    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * movementSpeed);
    }

    private void TurnPlayer()
    {
        if (Physics.BoxCast(playerView.transform.position, new Vector3(0.5f, 0.5f, 0.5f), transform.forward, out hit, Quaternion.identity, 1.5f, myMask))
        {
            transform.LookAt(transform.position - hit.normal, Vector3.up); //current position - the perpendicular vector returned by the hit.normal will turn the player towards the angled wall

            //look for the walls left and right
            rightCast = Physics.BoxCast(playerView.transform.position, new Vector3(0.5f, 0.5f, 0.5f), transform.right, Quaternion.identity, 4.0f, myMask);
            leftCast = Physics.BoxCast(playerView.transform.position, new Vector3(0.5f, 0.5f, 0.5f), -transform.right, Quaternion.identity, 4.0f, myMask);
            if (!rightCast && leftCast)
            {
                transform.Rotate(Vector3.up, 90);
            }
            else if (rightCast && !leftCast)
            {
                transform.Rotate(Vector3.up, -90);
            }
            else if (leftCast && rightCast)
            {
                transform.Rotate(Vector3.up, 180);
            }
            else
            {
                transform.Rotate(Vector3.up, 90);
            }
        }
    }

    public void MoveTowards(GameObject target)
    {
        mystate = state.ChasingEnemy;
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
    }

    public void MoveAway(GameObject target)
    {
        // Make the object look at the target
        transform.LookAt(target.transform.position, Vector3.up);

        // Zero out any X or Z axis rotation to prevent tilting (keep only Y-axis rotation)
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = 0;
        eulerRotation.z = 0;

        // Apply the adjusted rotation back to the transform
        transform.eulerAngles = eulerRotation;

        // Rotate the object 180 degrees around the Y-axis to face away from the target
        transform.Rotate(Vector3.up, 180);
    }
}
