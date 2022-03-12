using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] GameObject pfBlock;
    [SerializeField] Vector3 position;
    [SerializeField] int Width;
    [SerializeField] int Height;

    private bool[,] isOccupied;

    


    // Start is called before the first frame update
    void Start()
    {
        transform.position = position;
        isOccupied = new bool[Width, Height];

        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SpawnBlock();
        }
    }



    void SpawnBlock()
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
