using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering;

public class Character : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private MoveData presentMove;
    private List<MoveData> moveQueue;
    private int index;
    private bool moveEnd = true;
    public float descent = 2f;

    private int curStartCount = 0;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveStart()
    {
        if(curStartCount >= GameManager.Instance.GetRemainStartCount())
        {
            UIManager.Instance.ShowRetryUI();
            return;
        }

        moveQueue = playerInput.GetMoveQueue();
        index = 0;

        if (moveQueue.Count < 1)
        {
            return;
        }

        presentMove = moveQueue[0];
        moveEnd = false;

        GameManager.Instance.SetRemainStartCount();
    }

    void FixedUpdate()
    {
        if (!moveEnd)
        {
            Vector2 moveDelta = presentMove.direction * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + moveDelta);

            presentMove.time -= descent * Time.fixedDeltaTime;
            Debug.Log("�ð�: " + presentMove.time.ToString("F2"));

            if (presentMove.time <= 0)
            {
                index += 1;
                if (index < moveQueue.Count)
                    presentMove = moveQueue[index];
                else
                {
                    moveEnd = true;
                    moveQueue.Clear();
                    playerInput.ResetMoveQueue();
                    UIManager.Instance.ClearArrow();
                }
            }
        }
    }


    public PlayerInput GetPlayerInput()
    {
        return playerInput;
    }

    public bool GetMoveEnd()
    {
        return moveEnd;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Vector2 wallNormal = collision.contacts[0].normal;
        Vector2 moveDelta = - wallNormal * 0.005f;
        rb.MovePosition(rb.position + moveDelta);

    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    moveEnd = true;
    //
    //    playerInput.ResetMoveQueue();
    //    UIManager.Instance.ClearArrow();
    //}
}
