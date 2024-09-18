using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public bool isPlayerA = false;
    public GameObject circle;

    private Rigidbody2D rb;
    private Vector2 playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerA)
        {
            PaddleAController();
        } else {
            PaddleBController();
        }
    }

    private void PaddleBController()
    {   
        // Paddle B is moved according to relative movement of the circle
        if (circle.transform.position.y > transform.position.y + 0.5f) {
            playerMovement = new Vector2(0, 1); // move upwards
        } else if (circle.transform.position.y < transform.position.y - 0.5f) {
            playerMovement = new Vector2(0, -1); // move downwards
        } else {
             playerMovement = new Vector2(0, 0);  // stop
        }
    }

    private void PaddleAController()
    {
        // move using w and s buttons 
        playerMovement = new Vector2(0, Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        // the velocity of the paddles are controlled by speed variable
        rb.velocity = playerMovement*speed;

    }
    
}
