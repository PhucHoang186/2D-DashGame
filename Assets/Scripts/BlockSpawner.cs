using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public static BlockSpawner Instance;
    // prefabs
    [SerializeField] Block normalBlockPrefab;
    [SerializeField] Block smallBlockPrefab;
    // clamp spawn position normal block
    [SerializeField] float minLeft = -2f;
    [SerializeField] float maxLeft = -1f;
    [SerializeField] float minRight = 1f;
    [SerializeField] float maxRight = 2f;
    // clamp spawn position small block
    [SerializeField] float minPos = 1f;
    [SerializeField] float maxPos = 2f;
    //
    [SerializeField] float maxSpawnRate =2f;
    [SerializeField] float minSpawnRate = 0.5f;
    [SerializeField] float spawnRateAmount = 0.4f;
    float currentSpawnRate;

    [SerializeField] float maxBlockSpeed = 6f;
    [SerializeField] float minBlockSpeed = 5f;
    [SerializeField] float blockSpeedAmount = 0.2f;
    float currentBlockSpeed;
    [SerializeField] int numberPrefabs = 5;
    // pooling block
    [HideInInspector]
    public Queue<Block> normalBlockQueue;
    [HideInInspector]
    public Queue<Block> smallBlockQueue;
    private void Awake()
    {
        if(Instance!=null)
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
        currentSpawnRate = maxSpawnRate;
        currentBlockSpeed = minBlockSpeed;
        smallBlockQueue = new Queue<Block>();
        normalBlockQueue = new Queue<Block>();
        StartCoroutine(SpawnBlockCo());
        for (int i = 0; i < numberPrefabs; i++)
        {
            Block newSmallBlock = Instantiate(smallBlockPrefab, new Vector2(Random.Range(minPos, maxPos), transform.position.y), Quaternion.identity);
            newSmallBlock.gameObject.SetActive(false);
            smallBlockQueue.Enqueue(newSmallBlock);
            
            Block newNormalBlock = Instantiate(normalBlockPrefab, new Vector2(Random.Range(minPos, maxPos), transform.position.y), Quaternion.identity);
            newNormalBlock.gameObject.SetActive(false);
            normalBlockQueue.Enqueue(newNormalBlock);
        }

    }

    IEnumerator SpawnBlockCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentSpawnRate);
            Block newBlock = null;
            float pickBlock = Random.Range(1, 11);
            if (pickBlock < 4)
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
                if (pickSide >= 5)
                {
                    newBlock.transform.position = new Vector2(Random.Range(minLeft, maxLeft), transform.position.y);
                }
                else
                {
                    newBlock.transform.position = new Vector2(Random.Range(minRight, maxRight), transform.position.y);
                }
            }
        }
    }
    void UpdateSpeed()
    {
        currentSpawnRate -= spawnRateAmount;
        currentSpawnRate = Mathf.Clamp(currentSpawnRate,minSpawnRate,maxSpawnRate);
        currentBlockSpeed += blockSpeedAmount;
        currentBlockSpeed = Mathf.Clamp(currentBlockSpeed, minBlockSpeed, maxBlockSpeed);
    }
    private void OnEnable()
    {
        GameManager.Instance.UpdateBlockspawner += UpdateSpeed;
    }
    private void OnDisable()
    {
        GameManager.Instance.UpdateBlockspawner -= UpdateSpeed;

    }
}
