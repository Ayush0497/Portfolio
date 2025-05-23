using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject followObject;
    [SerializeField] private Vector2 followOffset;
    [SerializeField] private float speed;
    private Vector2 threshold;
    private Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero; // Velocity reference for SmoothDamp

    private void Start()
    {
        threshold = CalculateThreshold();
        rb = followObject.GetComponent<Rigidbody2D>();
        velocity = rb.velocity;
    }

    private void Update()
    {
        if (rb != null)
        {
            Vector2 followPos = followObject.transform.position;
            float xDifference = Mathf.Abs(transform.position.x - followPos.x);
            float yDifference = Mathf.Abs(transform.position.y - followPos.y);

            Vector3 newPosition = transform.position;

            if (xDifference >= threshold.x)
            {
                newPosition.x = followPos.x;
            }

            if (yDifference >= threshold.y)
            {
                newPosition.y = followPos.y;
            }

            // 🚀 Move towards target position at a controlled speed
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(newPosition.x, newPosition.y, transform.position.z),
                speed * Time.deltaTime);
        }
    }

    private Vector2 CalculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 camThreshold = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        camThreshold.x -= followOffset.x;
        camThreshold.y -= followOffset.y;
        return camThreshold;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border = CalculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
}