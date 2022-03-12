using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    
    private Rigidbody rb;
    [SerializeField] private float speed = 1;

    public float Width { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Width = transform.localScale.x;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Input.GetAxis("Mouse X");
        float postion = transform.position.x + (movement * Time.deltaTime * speed);
        postion = Mathf.Clamp(postion, -20f, 20f); // TODO: Fix gameBoardWidth, no magic numbers
        transform.position = new Vector3(postion, transform.position.y, transform.position.z);
    }

    public void Hit()
    {

    }
}
