using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParentController : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * direction);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}
