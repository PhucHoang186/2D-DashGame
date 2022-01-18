using UnityEngine;
public class PlayerController : MonoBehaviour
{
    // player properties
    [SerializeField] float borderPos = 2.15f;
    [SerializeField] float playerSpeed = 20;
    // manage movement
    bool isMoving =false;
    int moveDir = 1; //move right

    bool wantToMove = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            wantToMove = true;
        }
        if(wantToMove && !isMoving)// make movement more sensitive
        {
            wantToMove = false;
            isMoving = true;
        }
    }
    void FixedUpdate()
    {
        if(isMoving)
        {
            MovePlayer();
        }
    }
    void MovePlayer()
    {
        if (isMoving)
        {
            transform.Translate( Vector3.right * moveDir * playerSpeed * Time.fixedDeltaTime);
            if (transform.position.x >= borderPos)
            {
                ScoreManager.Instance.UpdateScoreUI();
                transform.position = new Vector2(borderPos, transform.position.y);
                isMoving = false;
                moveDir = -moveDir;
            }
            else if (transform.position.x <= -borderPos)
            {
                ScoreManager.Instance.UpdateScoreUI();
                transform.position = new Vector2(-borderPos, transform.position.y);
                isMoving = false;
                moveDir = -moveDir;
            }
        }
    }

}
