using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnerManager : MonoBehaviour
{
    public static BlockSpawnerManager Instance;
    // prefabs
    [SerializeField] Block normalBlockPrefab;
    [SerializeField] Block smallBlockPrefab;
    // clamp spawn position normal block
    [SerializeField] float minLeft = -2f;
    [SerializeField] float maxLeft = -1f;
    [SerializeField] float minRight = 1f;
    [SerializeField] float maxRight = 2f;
    // clamp spawn position small block
    [SerializeField] float minPos = -1f;
    [SerializeField] float maxPos = 1f;
    //control spawn block rate
    [SerializeField] float distanceBlock = 2;
    float currentSpawnRate;
    //control block speed
    [SerializeField] float maxBlockSpeed = 7f;
    [SerializeField] float minBlockSpeed = 6f;
    [SerializeField] float blockSpeedAmount = 0.2f;
    float currentBlockSpeed;
    [SerializeField] int numberPrefabs = 5;
    // pooling block
    [HideInInspector]
    public Queue<Block> normalBlockQueue;
    [HideInInspector]
    public Queue<Block> smallBlockQueue;
    //new block
    Block newBlockSpawn;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        currentBlockSpeed = minBlockSpeed;
        smallBlockQueue = new Queue<Block>();
        normalBlockQueue = new Queue<Block>();
        for (int i = 0; i < numberPrefabs; i++)
        {
            Block newSmallBlock = Instantiate(smallBlockPrefab, new Vector2(Random.Range(minPos, maxPos), transform.position.y), Quaternion.identity);
            newSmallBlock.gameObject.SetActive(false);
            smallBlockQueue.Enqueue(newSmallBlock);

            Block newNormalBlock = Instantiate(normalBlockPrefab, new Vector2(Random.Range(minPos, maxPos), transform.position.y), Quaternion.identity);
            newNormalBlock.gameObject.SetActive(false);
            normalBlockQueue.Enqueue(newNormalBlock);
        }
        newBlockSpawn = SpawnBlock();
    }
    private void Update()
    {
        CheckBlockDistance();
    }
    Block SpawnBlock()
    {
        Block newBlock = null;
        float pickBlock = Random.Range(1, 11);
        if (pickBlock < 4) // pick which block to spawn
        {
            newBlock = smallBlockQueue.Dequeue();
            newBlock.gameObject.SetActive(true);
            newBlock.defaultSpeed = currentBlockSpeed;
            newBlock.transform.position = new Vector2(Random.Range(minPos, maxPos), transform.position.y);
        }
        else
        {
            float pickSide = Random.Range(1, 11);
            newBlock = normalBlockQueue.Dequeue();
            newBlock.gameObject.SetActive(true);
            newBlock.defaultSpeed = currentBlockSpeed;
            if (pickSide >= 5) // pickwhich side to spawn block
            {
                newBlock.transform.position = new Vector2(Random.Range(minLeft, maxLeft), transform.position.y);
            }
            else
            {
                newBlock.transform.position = new Vector2(Random.Range(minRight, maxRight), transform.position.y);
            }
        }
        return newBlock;
    }
    public void UpdateBlockSpeed()
    {
        currentBlockSpeed += blockSpeedAmount;
        currentBlockSpeed = Mathf.Clamp(currentBlockSpeed, minBlockSpeed, maxBlockSpeed);
        Debug.Log(currentBlockSpeed);
    }
    private void OnEnable ()
    {
        GameManager.UpdateBlockspawner += UpdateBlockSpeed;
    }
    private void OnDisable()
    {
        GameManager.UpdateBlockspawner -= UpdateBlockSpeed;
    }
    void CheckBlockDistance()
    {   
        float distanceBetweenBlock = Mathf.Abs(transform.position.y -  newBlockSpawn.transform.position.y);
        if (distanceBetweenBlock >= distanceBlock)
        {
            newBlockSpawn = SpawnBlock();
        }
    }
}
