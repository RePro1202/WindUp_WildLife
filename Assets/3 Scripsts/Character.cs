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

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveStart()
    {
        moveQueue = playerInput.GetMoveQueue();
        index = 0;

        if(moveQueue.Count > 0)
            presentMove = moveQueue[0];

        moveEnd = false;
    }

    void FixedUpdate()
    {
        if (!moveEnd)
        {
            Vector2 moveDelta = presentMove.direction * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + moveDelta);

            presentMove.time -= descent * Time.fixedDeltaTime;
            Debug.Log("½Ã°£: " + presentMove.time.ToString("F2"));

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

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    moveEnd = true;
    //
    //    playerInput.ResetMoveQueue();
    //    UIManager.Instance.ClearArrow();
    //}
}
