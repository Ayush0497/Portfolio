using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuardPathFinder : MonoBehaviour
{
    [SerializeField] GameObject startNode, endNode, targetNode, currentNode, destinationNode;
    [SerializeField] List<GameObject> previousNode, waypointNode;
    [SerializeField] int previousNumber = 5;

    int waypointIndex;
    [SerializeField] TextMeshProUGUI guardState;
    public enum state
    {
        Patrolling,
        CapturedSomeone
    }

    public state mystate = state.Patrolling;
    // Start is called before the first frame update
    void Start()
    {
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
        if (guardState != null)
        {
            if (mystate == state.Patrolling)
            {
                guardState.text = "Guard State: Patrolling";
            }
            if (mystate == state.CapturedSomeone)
            {
                guardState.text = "Guard State: Captured Intruder";
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
    }
}
