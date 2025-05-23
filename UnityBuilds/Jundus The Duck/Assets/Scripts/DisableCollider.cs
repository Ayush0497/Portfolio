//using Unity.Burst.CompilerServices;
//using UnityEngine;

//public class DisableCollider : MonoBehaviour
//{
//    // Declare public variables to set in the Unity Inspector
//    public LayerMask groundLayer; // Layer mask for ground collisions
//    public JundusInLevel jundus;

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        // Check if the colliding object has the "Player" tag
//        if (collision.gameObject.CompareTag("Player"))
//        {
//            // Get the box collider component
//            BoxCollider2D boxCollider = jundus.GetComponent<BoxCollider2D>();

//            if (boxCollider != null)
//            {
//                // Perform the raycast from the top of the jundus
//                RaycastHit2D hit = Physics2D.Raycast((Vector2)jundus.transform.position + new Vector2(0, (boxCollider.size.y * jundus.transform.localScale.y) / 2), Vector2.right, jundus.boxCollider.size.x / 2 + 0.5f, groundLayer); // Adjust distance as needed

//                // If there's a collider in the way, disable this collider
//                if (hit.collider == null)
//                {
//                    Debug.Log("Hitting Forward");

//                    //GetComponent<Collider2D>().enabled = false;
//                }
//            }
//        }
//    }


//    private void OnDrawGizmos()
//    {
//        if (jundus != null)
//        {
//            BoxCollider2D boxCollider = jundus.GetComponent<BoxCollider2D>();
//            if (boxCollider != null)
//            {
//                // Calculate the top position of the jundus collider
//                Vector2 topPosition = (Vector2)jundus.transform.position + new Vector2(0, (boxCollider.size.y * jundus.transform.localScale.y) / 2);

//                // Draw the raycast to the right for visualization
//                Gizmos.color = Color.blue; // Change color to make it visible
//                float raycastDistance = (boxCollider.size.x * jundus.transform.localScale.x) / 2 + 0.2f; // Calculate distance for Gizmos
//                Gizmos.DrawLine(topPosition, topPosition + Vector2.right * raycastDistance); // Raycast to the right
//            }
//        }
//    }
//}