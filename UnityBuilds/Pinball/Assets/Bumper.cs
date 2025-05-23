using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float force;
    public Color colorStart;
    public Color colorEnd;
    public Renderer rend;
    public float duration;
    public GameState gameState;
    public Animator bumperAnimation;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = colorStart;
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bumperAnimation != null)
        {
            bumperAnimation.SetTrigger("Animate");
        }
        bumperAnimation.SetTrigger("Don'tAnimate");
        gameState.score = gameState.score + 5;
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        rend.material.color = Color.Lerp(colorStart, colorEnd, lerp);
        collision.rigidbody.AddForce(force * collision.relativeVelocity);
    }
}
