using UnityEngine;

public class Flipper : MonoBehaviour
{
    private HingeJoint2D joint2D;
    public GameState gameState;
    public enum FlipperType
    {
        Right,
        Left
    }
    public FlipperType type;
    private void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
        joint2D  = GetComponent<HingeJoint2D>();
    }
    private void Update()
    {
        switch(type)
        {
            case FlipperType.Right:
                joint2D.useMotor = Input.GetKey(KeyCode.RightArrow);
                break;

            case FlipperType.Left:
                joint2D.useMotor = Input.GetKey(KeyCode.LeftArrow);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if(collision.collider.tag.Equals("Ball"))
         {
            gameState.score++;
         }
    }
}
