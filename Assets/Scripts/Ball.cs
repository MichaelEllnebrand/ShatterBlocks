using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            speed = 30f;
            rb.AddForce(new Vector3(0,speed,0),ForceMode.Impulse);
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
                    //Debug.DrawRay(contact.point, newDirection, Color.red, 2f);
                }

            }
        }
    }
}
