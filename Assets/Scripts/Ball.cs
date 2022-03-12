using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;


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
            rb.AddForce(new Vector3(0,0,10f),ForceMode.Impulse);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Block block = collision.gameObject.GetComponent<Block>();
        if (block != null)
        {
            block.Hit();
        }
    }

    
}
