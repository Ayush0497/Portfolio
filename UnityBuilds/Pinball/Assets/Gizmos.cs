using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_SpawnPoint : MonoBehaviour
{
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}
