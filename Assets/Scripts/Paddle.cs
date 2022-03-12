using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    
    [SerializeField] private Board board;
    [SerializeField] private float speed = 1;

    public float Width { get; private set; }

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Width = transform.localScale.x;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {

        float movement = Input.GetAxis("Mouse X");
        float postion = transform.position.x + (movement * Time.deltaTime * speed);
        float maxPostion = (board.Width - Width) / 2;
        postion = Mathf.Clamp(postion, -maxPostion, maxPostion); 
        transform.position = new Vector3(postion, transform.position.y, transform.position.z);
    }

    public void Hit()
    {

    }
}
