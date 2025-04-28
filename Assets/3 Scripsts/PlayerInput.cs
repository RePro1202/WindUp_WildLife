using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const int MAX_QUEUE_LENGTH = 8;

    private List<MoveData> moveList = new List<MoveData>();

    private Character character;

    private KeyCode? currentPressedKey = null;
    private float keyDownTime = 0f;
    private float heldTime = 0f;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Update()
    {
        if (character.GetIsMoving())
        {
            return;
        }

        if (currentPressedKey.HasValue)
        {
            HandleKeyHold();
        }
        else if (!(GameManager.Instance.GetTimeOut()))
        {
            if (MAX_QUEUE_LENGTH <= moveList.Count)
            {
                return;
            }

            HandleKeyPress();
        }
    }

    private void HandleKeyHold()
    {
        heldTime = Time.time - keyDownTime;

        // 남은 시간보다 오래 누르면 남은 시간으로 고정.
        float remainTime = GameManager.Instance.GetRemainMoveTime();
        if (remainTime <= heldTime)
        {
            heldTime = remainTime;
        }

        UIManager.Instance.UpdateLastArrowTime(heldTime);

        // 키 입력 종료된 시점에, 이동 리스트에 추가.
        if (Input.GetKeyUp(currentPressedKey.Value))
        {
            Vector2 moveDir = KeyToDirection(currentPressedKey.Value);
            moveList.Add(new MoveData(moveDir, heldTime));

            updateRemainTime(heldTime);

            currentPressedKey = null;
        }
    }

    // 입력키 추가할 경우 해당 함수에 추가.
    private void HandleKeyPress()
    {
        if (CheckKeyPress(KeyCode.W, EKeyType.W, Vector2.up)) return;
        if (CheckKeyPress(KeyCode.A, EKeyType.A, Vector2.left)) return;
        if (CheckKeyPress(KeyCode.S, EKeyType.S, Vector2.down)) return;
        if (CheckKeyPress(KeyCode.D, EKeyType.D, Vector2.right)) return;
    }

    private bool CheckKeyPress(KeyCode key, EKeyType keyType, Vector2 direction)
    {
        if (Input.GetKeyDown(key))
        {
            currentPressedKey = key;
            keyDownTime = Time.time;

            UIManager.Instance.SpawnArrow(keyType, heldTime);

            return true;
        }

        return false;
    }

    private Vector2 KeyToDirection(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.W: return Vector2.up;
            case KeyCode.A: return Vector2.left;
            case KeyCode.S: return Vector2.down;
            case KeyCode.D: return Vector2.right;
            default: return Vector2.zero;
        }
    }

    private void updateRemainTime(float heldTime)
    {
        GameManager.Instance.SetRemainMoveTime(heldTime);
    }

    public List<MoveData> GetMoveQueue()
    {
        return moveList;
    }

    public float GetCurHeldTime()
    {
        return heldTime;
    }

    public void ResetMoveQueue()
    {
        moveList.Clear();
    }
}
