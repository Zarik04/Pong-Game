using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public float initialSpeed = 10f;
    public float speedIncrease = 0.2f;
    public Text playerText;
    public Text opponentText;

    // Game Sounds
    public AudioClip paddleHitSound;
    public AudioClip borderHitSound;
    public AudioClip scoreSound;
    // public AudioClip backgroundSound;

    private int hitCounter;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Invoking method with delay of 2 secs
        Invoke("StartBall", 2f);
    }

    
    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, initialSpeed + (speedIncrease*hitCounter));
    }

    private void StartBall() 
    {
        rb.velocity = new Vector2(-1, 0) * (initialSpeed + speedIncrease * hitCounter);
    }

    private void RestartBall()
    {   
        // speed to 0
        rb.velocity = new Vector2(0, 0);

        // restart origin
        transform.position = new Vector2(0, 0);

        // reset the hit counter
        hitCounter = 0;

        // Invoking method with delay of 2 secs
        Invoke("StartBall", 2f);
    }

    private void PlayerBounce(Transform obj) 
    {
        hitCounter++;

        Vector2 ballPosition = transform.position;
        Vector2 playerPosition = obj.position;

        float xDirection;
        float yDirection;

        // flipping the direction once ball hits the edge
        if (transform.position.x > 0) {
            xDirection = -1;
        } else {
            xDirection = 1;
        }

        // avoiding to get stuck
        yDirection = (ballPosition.y - playerPosition.y)/obj.GetComponent<Collider2D>().bounds.size.y;

        if (yDirection == 0) {
            yDirection = 0.25f;
        }

        // applying new directions to the ball
        rb.velocity = new Vector2(xDirection, yDirection) * (initialSpeed + (speedIncrease * hitCounter));
    }

    // calling bounce function when ball hits the one of the paddles
    private void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.name == "PaddleA" || other.gameObject.name == "PaddleB") {
            PlayerBounce(other.transform);
            SoundManager.instance.PlaySound(paddleHitSound);
        } else {
            SoundManager.instance.PlaySound(borderHitSound);
        }
    } 

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (transform.position.x > 0) {
            SoundManager.instance.PlaySound(scoreSound);
            RestartBall();
            opponentText.text = (int.Parse(opponentText.text) + 1).ToString();
        } else if (transform.position.x < 0) {
            SoundManager.instance.PlaySound(scoreSound);
            RestartBall();
            playerText.text = (int.Parse(playerText.text) + 1).ToString();
        }
    }
}
