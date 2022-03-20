using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paddle : MonoBehaviour
{
    
    [SerializeField] private Board board;

    public float Width { get; private set; }

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Width = transform.localScale.x;
    }

    void Update()
    {
        float movement = Input.GetAxis("Mouse X");
        float postion = transform.position.x + movement;
        float maxPostion = (board.Width - Width) / 2;
        postion = Mathf.Clamp(postion, -maxPostion, maxPostion); 
        transform.position = new Vector3(postion, transform.position.y, transform.position.z);
    }

    public void Hit()
    {

    }
}
