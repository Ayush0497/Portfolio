using UnityEngine;
using System.Collections;

public class JundusInLevel : MonoBehaviour
{
    public float speed = 30f;
    public float jumpForce = 30f;
    public LayerMask groundLayer;
    public LayerMask obstacleLayer; // Layer to check for obstacles above
    public Transform groundCheck;
    public Animator animator;
    public CapsuleCollider2D capsuleCollider; // CapsuleCollider2D for the player
    public BoxCollider2D leftCameraBoundary; // Boundary for the camera

    public Vector2 smallerColliderSize = new Vector2(7.0f, 4f); // Size for crouching
    public Vector2 standingColliderSize = new Vector2(10.0f, 8f); // Size for standing
    public Vector2 smallerColliderOffset = new Vector2(1.48f, -1.6f); // Offset for crouching
    public Vector2 standingColliderOffset = new Vector2(0f, 0f); // Offset for standing

    public bool allowSpriteFlip = true;
    public bool InLevel = true;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isCrouching;
    private bool shouldUncrouch = false;
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
        if (leftCameraBoundary != null)
        {
            leftCameraBoundary.enabled = true;
        }
        isCrouching = false;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Check for crouching input
        if (Input.GetKey(KeyCode.S))
        {
            if (!isCrouching)
            {
                isCrouching = true; // Start crouching
            }
        }
        else
        {
            shouldUncrouch = true; // Set the uncrouch flag
        }

        // Check for colliders above the player while crouching
        if (isCrouching && IsCollidingAbove())
        {
            Debug.Log("Collider detected above, remaining crouched.");
            AdjustCollider(smallerColliderSize, smallerColliderOffset);
            animator.Play("Crouch"); // Maintain crouch animation
            return; // Exit early to avoid handling movement when stuck
        }

        // Allow standing up if not colliding above and released S
        if (shouldUncrouch && isGrounded)
        {
            isCrouching = false;
            AdjustCollider(standingColliderSize, standingColliderOffset);
            shouldUncrouch = false; // Reset uncrouch flag
        }

        // Player movement
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Apply gravity for falling
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * Time.deltaTime;
        }

        // Flip sprite based on direction
        if (allowSpriteFlip)
        {
            if (moveInput > 0)
                transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            else if (moveInput < 0)
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Update animator parameter
        if (animator.HasParameter("isGrounded"))
        {
            animator.SetBool("isGrounded", isGrounded);
        }

        // Handle animations based on state
        if (InLevel)
        {
            HandleInLevelAnimations(moveInput);
        }
        else
        {
            HandleStandardAnimations(moveInput);
        }

        // BoxCast implementation for detecting obstacles in front from the top of the capsule collider
        float boxHeight = 0.2f; // Reduced height of the box (20% of the original)
        float boxWidth = capsuleCollider.size.x * this.transform.localScale.x * 0.2f; // Reduced width of the box (20% of the original)
        float rightOffset = 0.1f; // Offset to move the box to the right

        Vector2 boxOrigin = (Vector2)this.transform.position + new Vector2((capsuleCollider.size.x * this.transform.localScale.x / 2) + rightOffset, (capsuleCollider.size.y * this.transform.localScale.y / 2) + (boxHeight / 2) - 0.1f); // Box cast from the top, slightly lowered and moved to the right
        Vector2 boxSize = new Vector2(boxWidth, boxHeight); // Size of the box
        float boxDistance = 0.2f; // Distance to cast

        RaycastHit2D hit = Physics2D.BoxCast(boxOrigin, boxSize, 0f, Vector2.right, boxDistance, groundLayer);

        // If there's a collider in the way, disable this collider
        if (hit.collider != null)
        {
            Debug.Log("Hitting Forward");
            if (leftCameraBoundary != null)
            {
                leftCameraBoundary.enabled = false;
            }
             // Disable camera boundary
        }
        else
        {
            if(leftCameraBoundary != null)
            {
                leftCameraBoundary.enabled = true; // Enable camera boundary
            }
        }
    }

    private void HandleInLevelAnimations(float moveInput)
    {
        // Animation and collider adjustments
        if (isGrounded)
        {
            if (isCrouching)
            {
                animator.Play("Sneak1");
                AdjustCollider(smallerColliderSize, smallerColliderOffset); // Adjust for sneak animation
            }
            else
            {
                animator.Play("Run");
                AdjustCollider(standingColliderSize, standingColliderOffset); // Restore for standing
            }
        }
        else
        {
            if (isCrouching)
            {
                animator.Play("Crouch");
                AdjustCollider(smallerColliderSize, smallerColliderOffset); // Adjust for crouch
            }
            else
            {
                animator.Play("JumpForward");
                AdjustCollider(standingColliderSize, standingColliderOffset); // Restore during jump
            }
        }
    }

    private void HandleStandardAnimations(float moveInput)
    {
        if (isGrounded)
        {
            if (isCrouching)
            {
                animator.Play(moveInput != 0 ? "Sneak1" : "Crouch");
                AdjustCollider(smallerColliderSize, smallerColliderOffset);
            }
            else
            {
                animator.Play(moveInput != 0 ? "Run" : "Idle");
                AdjustCollider(standingColliderSize, standingColliderOffset);
            }
        }
        else
        {
            animator.Play("JumpForward");
            AdjustCollider(standingColliderSize, standingColliderOffset);
        }
    }

    private bool IsCollidingAbove()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 0.6f, obstacleLayer);
        return hit.collider != null; // Returns true if colliding with an obstacle above
    }

    private void AdjustCollider(Vector2 newSize, Vector2 newOffset)
    {
        // Adjust the CapsuleCollider2D size and offset
        capsuleCollider.size = newSize;
        capsuleCollider.offset = newOffset;
    }

    public void resetPlayer()
    {
        this.gameObject.tag = "NotPlayer";
        StartCoroutine(ResetPlayer());
    }

    private IEnumerator ResetPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.tag = "Player";
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnDrawGizmos()
    {
        if (this.gameObject != null)
        {
            // Raycast visualization for colliding above
            Gizmos.color = Color.red; // Change color to make it visible
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * 0.6f); // Raycast length

            // BoxCast visualization
            float boxHeight = 0.2f; // Adjusted height to visualize reduced box height
            float boxWidth = capsuleCollider.size.x * this.transform.localScale.x * 0.2f; // Adjusted width to visualize reduced box width
            float rightOffset = 0.1f; // Offset to move the box to the right
            Vector2 boxOrigin = (Vector2)this.transform.position + new Vector2((capsuleCollider.size.x * this.transform.localScale.x / 2) + rightOffset, (capsuleCollider.size.y * this.transform.localScale.y / 2) + (boxHeight / 2) - 0.1f); // Start from the top of the capsule collider

            if(isCrouching)
            {
                boxOrigin.y = boxOrigin.y - 0.13f;
            }

            // Draw the box for visualization
            Gizmos.color = Color.blue; // Set the color for the Gizmos box
            Gizmos.DrawWireCube(boxOrigin, new Vector2(boxWidth, boxHeight)); // Visualize the box
        }
    }
}

public static class AnimatorExtensions
{
    public static bool HasParameter(this Animator animator, string paramName)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName)
                return true;
        }
        return false;
    }
}