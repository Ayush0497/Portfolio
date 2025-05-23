using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] Rigidbody rbody;
    [SerializeField] GameObject detector;
    [SerializeField] float forceAmount = 10;
    [SerializeField] LayerMask myMask;
    [SerializeField] GameObject playerView;
    [SerializeField] bool leftCast, rightCast;

    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * movementSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wall")
        {
            if (Physics.BoxCast(playerView.transform.position, new Vector3(0.5f, 0.5f, 0.5f), transform.forward, Quaternion.identity, 1.5f, myMask))
            {
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
                else if(leftCast && rightCast)
                {
                    transform.Rotate(Vector3.up, 180);
                }
                else
                {
                    transform.Rotate(Vector3.up, 90);
                }             
            }
                
            else
            {
                rbody.AddForce(Vector3.up * forceAmount, ForceMode.Impulse);
            }
        }
    }

}
