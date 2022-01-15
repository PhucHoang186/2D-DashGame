using UnityEngine;
public class Block : MonoBehaviour
{
    public float defaultSpeed;
    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        transform.Translate( new Vector3(0f, -defaultSpeed * Time.deltaTime),Space.World);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            ScoreManager.Instance.SaveBestScore();
            GameManager.Instance.LostGame();
        }
    }
}
