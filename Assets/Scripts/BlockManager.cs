using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockManager : MonoBehaviour
{
    [SerializeField] GameObject pfBlock;
    [SerializeField] Material[] materials;
    [SerializeField] Material defaultMaterial;
    [SerializeField] GameObject ghost;
    [SerializeField] Vector3 position;
    [SerializeField] int Width;
    [SerializeField] int Height;

    private float columnWidth = 3.0f;
    private bool[,] isOccupied;

    [SerializeField] private float spawnTimerMax;
    private float spawnTimer;
    private Image spawnTimerImage;
    private int ghostColumn = 6;

    private GameManager gameManager;

    void Awake()
    {
        transform.position = position;
        isOccupied = new bool[Width, Height];
        spawnTimer = spawnTimerMax;
    }

    void Start()
    {
        spawnTimerImage = GameObject.Find("SpawnTimerImage").GetComponent<Image>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        for (int i = 0; i < 30; i++)
        {
            SpawnBlockAtRandomPosition();
        }

    }

    void Update()
    {
        if (gameManager.IsGameRunning)
        {
            UpdateBlockSpawner();

            if (Input.GetKeyDown(KeyCode.A))
            {
                ghostColumn--;
                if (ghostColumn < 0) ghostColumn = 0;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                ghostColumn++;
                if (ghostColumn > Width - 1) ghostColumn = Width - 1;
            }
        }
        SetGhostPosition();
    }

    private void UpdateBlockSpawner()
    {
        spawnTimerImage.fillAmount = spawnTimer / spawnTimerMax;
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            spawnTimer += spawnTimerMax;
            if (!SpawnBlockAtGhost())
            {
                gameManager.GameOver("Column full, failed to spawn block!");
            }
            SpawnBlockAtRandomPosition();
        }
    }

    void SetGhostPosition()
    {
        ghost.transform.position = new Vector3(position.x + ghostColumn * columnWidth, ghost.transform.position.y, ghost.transform.position.z);
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
                SpawnBlock(x,y,materials[y]);
            }
        }
        return isSpawned;
    }

    void SpawnBlockAtRandomPosition()
    {
        int x = Random.Range(0,Width);
        int y = Random.Range(0,Height);
        if (!isOccupied[x, y])
        {
            //SpawnBlock(x,y,defaultMaterial);
            SpawnBlock(x, y, materials[y]);
        }
    }

    void SpawnBlock(int x, int y, Material material)
    {
        if (isOccupied[x, y]) return;

        isOccupied[x, y] = true;
        Vector3 pos = new Vector3(position.x + x * pfBlock.transform.localScale.x, position.y + y * pfBlock.transform.localScale.y, 0);
        GameObject b = Instantiate(pfBlock, pos, Quaternion.identity, transform);
        b.transform.position = pos;
        b.GetComponent<Block>().SetPostion(x, y);
        b.GetComponent<Renderer>().material = material;
        b.GetComponent<ParticleSystemRenderer>().material = material;
        b.GetComponent<ParticleSystem>().Play();
    }

    public void FreeLocation(int x, int y)
    {
        isOccupied[x, y] = false;
    }
}