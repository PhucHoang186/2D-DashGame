using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnerManager : MonoBehaviour
{
    public static BlockSpawnerManager Instance;
    // prefabs
    [SerializeField] Block normalBlockPrefab;
    [SerializeField] Block smallBlockPrefab;
    [SerializeField] Block coinPrefab;
    [Header("Set Clamp Spawner")]
    // clamp spawn position normal block
    [SerializeField] float minLeft = -2f;
    [SerializeField] float maxLeft = -1f;
    [SerializeField] float minRight = 1f;
    [SerializeField] float maxRight = 2f;
    // clamp spawn position small block
    [SerializeField] float minPos = -1f;
    [SerializeField] float maxPos = 1f;
    [Header("Block Properties")]
    //control spawn block rate
    [SerializeField] float distanceBlock = 2;
    //control spawn coin rate
    [SerializeField] float distanceCoin = 2;
    //control block speed
    [SerializeField] float maxBlockSpeed = 7f;
    [SerializeField] float minBlockSpeed = 6f;
    [SerializeField] float blockSpeedAmount = 0.2f;
    float currentBlockSpeed;
    [SerializeField] int numberPrefabs = 5;
    // pooling block
    [HideInInspector]
    public Queue<Block> normalBlockQueue;// normal block
    [HideInInspector]
    public Queue<Block> smallBlockQueue;// small block
    [HideInInspector]
    public Queue<Block> coinQueue;// Coin

    // contain block that being currently spawn
    [HideInInspector]
    public Queue<Block> currentBlockQueue;
    // contain coin that being currently spawn
    [HideInInspector]
    public Queue<Block> currentCoinSpawn;

    //new block being spawn
    Block newBlockSpawn;
    bool coinSpawn;
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
        coinQueue = new Queue<Block>();
        currentBlockQueue = new Queue<Block>();
        for (int i = 0; i < numberPrefabs; i++)
        {
            //Pooling small block
            Block newSmallBlock = Instantiate(smallBlockPrefab, Vector2.zero, Quaternion.identity);
            newSmallBlock.gameObject.SetActive(false);
            smallBlockQueue.Enqueue(newSmallBlock);
            //Pooling normal block
            Block newNormalBlock = Instantiate(normalBlockPrefab, Vector2.zero, Quaternion.identity);
            newNormalBlock.gameObject.SetActive(false);
            normalBlockQueue.Enqueue(newNormalBlock);
            //Pooling coins
            Block newCoin = Instantiate(coinPrefab, Vector2.zero, Quaternion.identity);
            newCoin.gameObject.SetActive(false);
            coinQueue.Enqueue(newCoin);
        }
        newBlockSpawn = SpawnBlock();
    }
    private void Update()
    {
        if(CheckBlockDistance(distanceBlock, newBlockSpawn.transform))
        {
            newBlockSpawn = SpawnBlock();
        }
        if (CheckBlockDistance(distanceCoin, newBlockSpawn.transform) && coinSpawn)
        {
            coinSpawn = false;
            SpawnCoin();
        }


    }
    Block SpawnBlock()
    {
        coinSpawn = true;
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
            if (pickSide >= 5) // pick which side to spawn block
            {
                newBlock.transform.position = new Vector2(Random.Range(minLeft, maxLeft), transform.position.y);
            }
            else
            {
                newBlock.transform.position = new Vector2(Random.Range(minRight, maxRight), transform.position.y);
            }
        }
        currentBlockQueue.Enqueue(newBlock);// to control all the active block's speed
        return newBlock;
    }

    void SpawnCoin()
    {
        Block newCoin = null;
        float spawn = Random.Range(1, 11); // chooe if spawn coin or not
        if (spawn > 7)
        {

            newCoin = coinQueue.Dequeue();
            newCoin.gameObject.SetActive(true);
            newCoin.transform.position = new Vector2(0, transform.position.y);
            newCoin.defaultSpeed = currentBlockSpeed;
            currentBlockQueue.Enqueue(newCoin);
        }
    }
    // update the speed of all the blocks
    public void UpdateBlockSpeed()
    {
        currentBlockSpeed += blockSpeedAmount;
        currentBlockSpeed = Mathf.Clamp(currentBlockSpeed, minBlockSpeed, maxBlockSpeed);
        foreach (Block currentBlock in currentBlockQueue)
        {
            currentBlock.defaultSpeed = currentBlockSpeed;
        }
    }
    private void OnEnable ()
    {
        GameManager.UpdateBlockspawner += UpdateBlockSpeed;
    }
    private void OnDisable()
    {
        GameManager.UpdateBlockspawner -= UpdateBlockSpeed;
    }
    //To keep the distance between blocks the same 
    bool CheckBlockDistance(float _distance, Transform _newPos)
    {   
        float distanceBetweenBlock = Mathf.Abs(transform.position.y - _newPos.position.y);
        if (distanceBetweenBlock >= _distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
