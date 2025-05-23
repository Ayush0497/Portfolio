using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickoutHole : MonoBehaviour
{
    public GameState gameState;
    public float force;
    public float duration;
    public Renderer rend;
    public Renderer rend2;

    private void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        rb.MovePosition(transform.position);
        rb.bodyType = RigidbodyType2D.Kinematic;
        rend = GameObject.FindGameObjectWithTag("Ball").GetComponent<Renderer>();
        rend2 = GameObject.FindGameObjectWithTag("Hole").GetComponent<Renderer>();
        rend.enabled = false;
        rend2.material.color = Color.red;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        gameState.score = gameState.score + 500;
        rend2.material.color = new Color(255, 255, 0);
        rend.enabled = true;
        Rigidbody2D rb = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(new Vector2(force, force), ForceMode2D.Impulse);
    }
}
