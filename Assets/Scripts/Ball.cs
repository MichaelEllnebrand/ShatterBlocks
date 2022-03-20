using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private GameManager gameManager;
    [SerializeField] private float speed = 30;
    [SerializeField] private float nudgeFactor = 3;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, speed, 0), ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > 0 && Mathf.Abs(rb.velocity.y) < nudgeFactor)
        {
            float directionX = rb.velocity.x > 0 ? nudgeFactor / speed : directionX = -(nudgeFactor / speed);
            float directionY = rb.velocity.y > 0 ? (speed - nudgeFactor) / speed : directionY = -((speed - nudgeFactor) / speed);

            Vector3 newDirection = new Vector3(directionX, directionY, 0).normalized;
            rb.velocity = newDirection * speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            Block block = collision.gameObject.GetComponent<Block>();
            if (block != null)
            {
                block.Hit();
            }
        }

        if (collision.gameObject.CompareTag("Paddle"))
        {
            Paddle paddle = collision.gameObject.GetComponent<Paddle>();
            if (paddle != null)
            {
                paddle.Hit();
                
                float adjustAngle = (transform.position.x - paddle.transform.position.x) / paddle.Width;

                foreach (ContactPoint contact in collision.contacts)
                {
                    Vector3 newDirection = new Vector3(contact.normal.x + adjustAngle, contact.normal.y, contact.normal.z).normalized;
                    rb.velocity = newDirection * speed;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OutOfBounds"))
        {
            gameManager.GameOver("Ball lost!");

        }
    }
}