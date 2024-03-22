using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D Rb;
    private Vector2 StartingVelocity = new Vector2(5f, 5f);
    public GameManager GameManager;

    public AudioSource audioSource;

    public void ResetBall()
    {
        transform.position = Vector3.zero;

        if (Rb == null)
            Rb = GetComponent<Rigidbody2D>();

        Rb.velocity = new Vector2(0f, 0f);

        Invoke(nameof(SetVelocity), 3);
    }

    void SetVelocity()
    {
        int xValue = Random.Range(1, 3);
        int yValue = Random.Range(1, 3);

        if (yValue == 1)
            yValue = 5;
        if (yValue == 2)
            yValue = -5;

        if (xValue == 1)
            xValue = 5;
        if (xValue == 2)
            xValue = -5;

        //Rb.velocity = StartingVelocity;
        Rb.velocity = new Vector2(xValue, yValue);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 newVelocity = Rb.velocity;
            newVelocity.y = -newVelocity.y;
            Rb.velocity = newVelocity;
        }

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            Rb.velocity = new Vector2(-Rb.velocity.x, Rb.velocity.y);
            Rb.velocity *= 1.1f;
            audioSource.Play();
        }

        if (collision.gameObject.CompareTag("WallEnemy"))
        {
            GameManager.ScoreEnemy();
            ResetBall();
        }
        else if (collision.gameObject.CompareTag("WallPlayer"))
        {
            GameManager.ScorePlayer();
            ResetBall();
        }
    }
}
