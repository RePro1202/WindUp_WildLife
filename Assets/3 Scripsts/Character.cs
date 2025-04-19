using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void MoveStart()
    {
        List<MoveData> moveQueue = playerInput.GetMoveQueue();

        StartCoroutine(ProcessMoveQueue(moveQueue));
    }

    IEnumerator ProcessMoveQueue(List<MoveData> moveQueue)
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
    }

    public PlayerInput GetPlayerInput()
    {
        return playerInput;
    }
}
