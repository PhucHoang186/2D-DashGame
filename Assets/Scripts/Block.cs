using UnityEngine;
public class Block : MonoBehaviour
{
    [HideInInspector]
    public float defaultSpeed;
    void FixedUpdate()
    {
        Movement();
    }

    public void Movement()
    {
        transform.Translate( new Vector3(0f, -defaultSpeed * Time.deltaTime),Space.World);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        BlockSpawnerManager.Instance.currentBlockQueue.Dequeue();
        if (collision.transform.CompareTag("Boundary"))
        {
            transform.position = Vector2.zero;
            gameObject.SetActive(false);
            if (gameObject.CompareTag("Small"))
            {
                BlockSpawnerManager.Instance.smallBlockQueue.Enqueue(this);
            }
            else if (gameObject.CompareTag("Normal"))
            {
                BlockSpawnerManager.Instance.normalBlockQueue.Enqueue(this);
            }
            else if(gameObject.CompareTag("Coin"))
            {
                BlockSpawnerManager.Instance.coinQueue.Enqueue(this);
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !(gameObject.layer == LayerMask.NameToLayer("Collectable")))
        {
            Destroy(gameObject);
            ScoreManager.Instance.SaveBestScore();
            GameManager.Instance.LostGame();
            PlayerStats.Instance.SavePlayerMoney();
        }
    }
}
