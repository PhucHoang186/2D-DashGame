using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class PlayerController : MonoBehaviour
{
    // player properties
    [SerializeField] float borderPos = 2.15f;
    [SerializeField] float playerSpeed = 20;
    // manage movement
    bool isMoving = false;
    int moveDir = 1; //move right


    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    // Update is called once per frame
    void Update()
    {

        if (IsPointerOverUIObject())
        {
            return;
        }
        if ((Input.touchCount > 0 || Input.GetMouseButtonDown(0)) && !isMoving)
        {
            isMoving = true;
            if (GameManager.Instance.startScreen != null && GameManager.Instance.startScreen.activeSelf)// turn off "press to start" text
            {
                GameManager.Instance.startScreen.SetActive(false);
            }
            else
            {
                return;
            }
        }

    }
    void FixedUpdate()
    {

        if (isMoving)
        {
            MovePlayer();
        }
    }
    void MovePlayer()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.right * moveDir * playerSpeed * Time.fixedDeltaTime);
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

