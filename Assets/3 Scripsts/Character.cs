using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering;

public class Character : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private MoveData presentMove;
    private List<MoveData> moveQueue;
    private int index = 0, indexcount;
    private bool moveEnd = true;
    public float descent = 0.4f;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void MoveStart()
    {
        moveQueue = playerInput.GetMoveQueue();
        indexcount = moveQueue.Count;
        presentMove = moveQueue[0];
        moveEnd = false;
        //StartCoroutine(ProcessMoveQueue(moveQueue));

    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
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
                if (index < indexcount)
                    presentMove = moveQueue[index];
                else
                    moveEnd = true;
            }
        }
    }

    /*IEnumerator ProcessMoveQueue(List<MoveData> moveQueue)
    {
        foreach (MoveData move in moveQueue)
        {
            Debug.Log($"Move: {move}");
            yield return StartCoroutine(MoveForTime(move.direction, move.time));
        }

        playerInput.ResetMoveQueue();
    }

    IEnumerator MoveForTime(Vector3 dir, float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            transform.position += dir.normalized * moveSpeed * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }
    }*/

    public PlayerInput GetPlayerInput()
    {
        return playerInput;
    }
}
