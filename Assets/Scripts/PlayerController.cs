using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    // player properties
    [SerializeField] float borderPos = 2.15f;
    [SerializeField] float playerSpeed = 20;
    // manage movement
    bool isMoving =false;
    int moveDir = 1; //move right


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)  && !isMoving)
        {
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
            transform.Translate( Vector3.right * moveDir * playerSpeed * Time.deltaTime);
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
