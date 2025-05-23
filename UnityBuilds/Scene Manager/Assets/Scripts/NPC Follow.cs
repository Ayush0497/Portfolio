using UnityEngine;

public class NPCFollow : MonoBehaviour
{
    public float baseFollowSpeed = 5f;  // Base speed of the NPC
    public float stoppingDistance = 2f; // Distance at which the NPC stops following
    public Animator animator;           // Reference to the NPC's Animator

    [SerializeField] ThirdPersonController player; // Reference to the player
    private bool isPlayerInRange = false; // Flag to check if the player is in range
    private Rigidbody rb;                // Reference to the NPC's Rigidbody

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
        if (rb != null)
        {
            rb.isKinematic = true; // Set Rigidbody to kinematic if not using physics
        }
        animator.SetBool("run", false);
    }

    private void Update()
    {
        // Only follow if the player is in range
        if (isPlayerInRange && player != null)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        // Calculate distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Stop moving if within stopping distance
        if (distanceToPlayer <= stoppingDistance)
        {
            animator.SetBool("run", false);
            return;
        }

        // Get player's velocity (assuming ThirdPersonController has a velocity variable)
        float playerSpeed = player.GetComponent<CharacterController>().velocity.magnitude;

        // Adjust NPC's speed to match player's speed
        float adjustedSpeed = Mathf.Max(baseFollowSpeed, playerSpeed); // Ensure NPC speed is never too slow

        // Move towards the player (Only on X-Z plane)
        Vector3 direction = (player.transform.position - transform.position).normalized;
        direction.y = 0; // Disable Y-axis movement

        Vector3 movement = direction * adjustedSpeed * Time.deltaTime;

        // Update position (Only on X-Z plane)
        transform.position += movement;

        // Rotate NPC to face the player
        if (movement.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        // Play running animation
        animator.SetBool("run", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // Enable following
        }
    }
}
