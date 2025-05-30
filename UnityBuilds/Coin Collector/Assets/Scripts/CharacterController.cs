using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CharacterController : MonoBehaviour
{
    public float maxSpeed = 5;
    public Transform groundCheck;
    public float jumpForce = 1000;

    protected Animator myAnimator;
    protected Rigidbody2D myRigidBody;
    protected float moveForce = 365;
    protected bool facingRight = true;
    protected bool grounded = false;
    protected bool jump = false;
    Tracking gameState;
    SoundHub soundHub;

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        gameState = GameObject.FindGameObjectWithTag("Gamestate").GetComponent<Tracking>();
        soundHub = GameObject.Find("SoundHub").GetComponent<SoundHub>();
    }

    void Update()
    {
        //layer mask bitwise ops: https://answers.unity.com/questions/8715/how-do-i-use-layermasks.html
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
        if (this.transform.position.x > 33 && this.transform.position.y < -2 && gameState.coins == 0)
        {
            gameState.won = true;
            SceneManager.LoadScene(1);
        }
    }

    void FixedUpdate()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        myAnimator.SetFloat("Speed", Mathf.Abs(horizontalAxis));
        //Have we reach maxSpeed? If not, add force.
        if(horizontalAxis > 0 && !facingRight || horizontalAxis < 0 && facingRight)
        {
            flip();
        }

        if (horizontalAxis * myRigidBody.velocity.x < maxSpeed)
        {
            myRigidBody.AddForce(Vector2.right * horizontalAxis * moveForce);
        }
        //have we exceeded the maxSpeed? Clamp it (set it to maxSpeed).
        if (Mathf.Abs(myRigidBody.velocity.x) > maxSpeed)
        {
            myRigidBody.velocity = new Vector2(Mathf.Sign(myRigidBody.velocity.x) * maxSpeed, myRigidBody.velocity.y);
        }

        if (jump)
        {
            soundHub.PlayJumpSound();
            myAnimator.SetTrigger("Jump");
            myRigidBody.AddForce(new Vector2(0, jumpForce));
            jump = false;
        }
    }

    private void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}





