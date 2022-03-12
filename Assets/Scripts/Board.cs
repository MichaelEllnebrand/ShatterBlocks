using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour 
{
    [SerializeField] GameObject leftGameBorder;
    [SerializeField] GameObject rightGameBorder;

    public float Width { get; private set; } = 40;
    public float Height { get; private set; }

    private void Awake()
    {
        float offset = (Width / 2) + (leftGameBorder.transform.localScale.y / 2);
        leftGameBorder.transform.position = new Vector3(-offset, leftGameBorder.transform.position.y, leftGameBorder.transform.position.z);
        rightGameBorder.transform.position = new Vector3(offset, rightGameBorder.transform.position.y, rightGameBorder.transform.position.z);
    }
}