using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private int _x;
    private int _y;

    private BlockManager blockManager;

    private void Awake()
    {
        blockManager = FindObjectOfType<BlockManager>();
        
    }

    public void SetPostion(int x, int y)
    {
        _x = x;
        _y = y;
    }


    public void Hit()
    {
        blockManager.FreeLocation(_x, _y);
        Destroy(gameObject);
    }

}
