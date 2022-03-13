using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] GameObject destroyBlockEffect;
    private int _x;
    private int _y;

    private BlockManager blockManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        blockManager = FindObjectOfType<BlockManager>();
    }

    public void SetPostion(int x, int y)
    {
        _x = x;
        _y = y;
    }


    public void Hit()
    {
        gameManager.AddScore();
        GameObject blockEffect =  Instantiate(destroyBlockEffect, transform.position, Quaternion.identity);
        blockEffect.GetComponent<ParticleSystem>().Play();
        Destroy(blockEffect,1f);
        blockManager.FreeLocation(_x, _y);
        Destroy(gameObject);
    }

}
