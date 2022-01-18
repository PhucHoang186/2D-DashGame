using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
  public void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerStats.Instance.money ++;
        ScoreManager.Instance.UpdateCoinUI();//update Coin UI
        BlockSpawnerManager.Instance.currentBlockQueue.Dequeue(); // Pop out of current block queue
        transform.position = Vector2.zero;
        gameObject.SetActive(false);
        BlockSpawnerManager.Instance.coinQueue.Enqueue(this.gameObject.GetComponent<Block>());

    }

}
