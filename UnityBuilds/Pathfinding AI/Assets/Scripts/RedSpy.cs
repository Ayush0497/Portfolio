using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RedSpy : MonoBehaviour
{
    [SerializeField] GameObject startNode, endNode, targetNode, currentNode, destinationNode;
    [SerializeField] List<GameObject> previousNode, waypointNode;
    [SerializeField] int previousNumber = 5;
    [SerializeField] bool captured = false;
    [SerializeField] TextMeshProUGUI RedSpyState;
    [SerializeField] TextMeshProUGUI ItemsInHand;
    public enum state
    {
        TravellingToDocument,
        TravellingToKey,
        TravellingToDestination,
        Evading,
        RoamingAround
    }
    public state mystate = state.TravellingToDocument;

    int waypointIndex;

    // Start is called before the first frame update
    void Start()
    {
        Events.CapturedRedSpy.AddListener(capturedSpy);
        transform.position = startNode.transform.position;
        currentNode = startNode;
        targetNode = currentNode;

        if (waypointNode.Count > 0)
        {
            destinationNode = waypointNode[waypointIndex];
        }
        else
        {
            destinationNode = endNode;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(RedSpyState!= null)
        {
            if (mystate == state.TravellingToDocument)
            {
                RedSpyState.text = "Red Spy State: Moving Towards Document";
            }
            else if (mystate == state.TravellingToKey)
            {
                RedSpyState.text = "Red Spy State: Moving Towards Power Up";
            }
            else if (mystate == state.TravellingToDestination)
            {
                RedSpyState.text = "Red Spy State: Moving Towards Destination";
            }
            else if (mystate == state.Evading)
            {
                RedSpyState.text = "Red Spy State: Evading, Jumping";
            }
            else if (mystate == state.RoamingAround)
            {
                RedSpyState.text = "Red Spy State: Reached Destination, Roaming Around";
            }
        }
        if (Vector3.Distance(transform.position, targetNode.transform.position) < 0.2f)
        {
            if (targetNode == destinationNode)
            {
                if (waypointNode.Count > 0)
                {
                    waypointIndex++;
                    if (waypointIndex >= waypointNode.Count)
                    {
                        waypointIndex = 0;
                    }
                    destinationNode = waypointNode[waypointIndex];
                }
                else
                {
                    if (destinationNode == endNode)
                    {
                        destinationNode = startNode;
                    }
                    else
                    {
                        destinationNode = endNode;
                    }
                }
            }

            previousNode.Add(currentNode);
            if (previousNode.Count > previousNumber)
            {
                previousNode.RemoveAt(0);
            }

            currentNode = targetNode;
            if (currentNode.GetComponent<Pathnode>().connections.Count > 0)
            {
                bool foundNode = false;
                int timesThrough = 0;
                float closeDistance = 10000;
                GameObject closestNode = null;

                while (!foundNode && timesThrough < 100)
                {
                    //int index = Random.Range(0, currentNode.GetComponent<Pathnode>().connections.Count);
                    for (int i = 0; i < currentNode.GetComponent<Pathnode>().connections.Count; i++)
                    {
                        if (!previousNode.Contains(currentNode.GetComponent<Pathnode>().connections[i]) && currentNode.GetComponent<Pathnode>().connections[i].GetComponent<Pathnode>().nodeActive)
                        {
                            if (Vector3.Distance(currentNode.GetComponent<Pathnode>().connections[i].transform.position, destinationNode.transform.position) < closeDistance)
                            {
                                closeDistance = Vector3.Distance(currentNode.GetComponent<Pathnode>().connections[i].transform.position, destinationNode.transform.position);
                                closestNode = currentNode.GetComponent<Pathnode>().connections[i];
                            }
                        }
                    }

                    if (closestNode != null)
                    {
                        targetNode = closestNode;
                        foundNode = true;
                    }
                    else
                    {
                        previousNode.Clear();
                    }


                    timesThrough++;
                }
            }
        }
        else
        {
            transform.Translate((targetNode.transform.position - transform.position).normalized * Time.deltaTime * 3.0f);
        }

        if (captured)
        {
            transform.position = startNode.transform.position;
            currentNode = startNode;
            targetNode = startNode;
            previousNode.Clear();
        }

        if (currentNode == endNode)
        {
            reached();
        }
    }

    void capturedSpy()
    {
        captured = true;
        StartCoroutine(ResetCapturedAfterDelay());
    }

    IEnumerator ResetCapturedAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        captured = false;
    }

    void run()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            float jumpForce = 12.0f;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void UpdateDocument()
    {
        mystate = state.TravellingToKey;
        ItemsInHand.text = "Items in Hand: None";
    }
    public void UpdateKey()
    {
        mystate = state.TravellingToDestination;
        ItemsInHand.text = "Items in Hand: Phase through PowerUp";
        StartCoroutine(ResetTextAfterDelay());
    }
    IEnumerator ResetTextAfterDelay()
    {
        yield return new WaitForSeconds(11f);
        ItemsInHand.text = "Items in Hand: None";
    }

    public void reached()
    {
        mystate = state.RoamingAround;
    }
}
