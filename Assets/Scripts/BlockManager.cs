using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] GameObject pfBlock;
    [SerializeField] Material[] materials;
    [SerializeField] GameObject ghost;
    [SerializeField] Vector3 position;
    [SerializeField] int Width;
    [SerializeField] int Height;

    private bool[,] isOccupied;
    
    [SerializeField] private float spawnTimerMax;
    private float spawnTimer;
    private int ghostColumn = 7;

    void Awake()
    {
        transform.position = position;
        isOccupied = new bool[Width, Height];
        spawnTimer = spawnTimerMax;

        foreach (Material mat in materials)
        {
            Debug.Log(mat);
        }
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            spawnTimer += spawnTimerMax;
            if (!SpawnBlockAtGhost())
            {
                Debug.Log("GAME OVER");
            }
        }
        

        if (Input.GetKeyDown(KeyCode.A))
        {
            ghostColumn--;
            if (ghostColumn < 0) ghostColumn = 0;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ghostColumn++;
            if (ghostColumn > Width-1) ghostColumn = Width-1;
        }
        SetGhostPosition();
    }

    void SetGhostPosition()
    {
        ghost.transform.position = new Vector3(position.x + ghostColumn * ghost.transform.localScale.x, ghost.transform.position.y, ghost.transform.position.z);
    }

    private bool SpawnBlockAtGhost()
    {
        bool isSpawned = false;
        int x = ghostColumn;
        for (int y = 0; y < Height; y++)
        {
            if (!isOccupied[x, y] && !isSpawned)
            {
                isSpawned = true;
                isOccupied[ghostColumn, y] = true;
                Vector3 pos = new Vector3(position.x + x * pfBlock.transform.localScale.x, position.y + y * pfBlock.transform.localScale.y, 0);
                GameObject b = Instantiate(pfBlock, pos, Quaternion.identity, transform);
                b.transform.position = pos;
                b.GetComponent<Renderer>().material = materials[y];
                b.GetComponent<Block>().SetPostion(x, y);
            }
        }
        return isSpawned;
    }

    void SpawnBlockRandom()
    {
        int x = Random.Range(0,Width);
        int y = Random.Range(0,Height);
        
        if (!isOccupied[x, y])
        {
            isOccupied[x, y] = true;
            Vector3 pos = new Vector3(position.x + x * pfBlock.transform.localScale.x, position.y + y * pfBlock.transform.localScale.y, 0);
            GameObject b = Instantiate(pfBlock, pos,Quaternion.identity,transform);
            b.transform.position = pos;
            b.GetComponent<Block>().SetPostion(x, y);
        }
    }

    public void FreeLocation(int x, int y)
    {
        if (isOccupied[x, y])
        {
            isOccupied[x, y] = false;
        }
    }
}
