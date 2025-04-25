using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering;
using UnityEngine.PlayerLoop;

public class Character : MonoBehaviour
{
    private PlayerInput playerInput;
    private CharacterMove characterMove;

    private bool isMoving = false;

    private int curStartCount = 0; // TODO : 다른 곳에서 처리 고려.

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        characterMove = GetComponent<CharacterMove>();
    }

    public void MoveEnd()
    {
        playerInput.ResetMoveQueue();
        UIManager.Instance.ClearArrow();
    }


    public void MoveStart()
    {
        if (curStartCount >= GameManager.Instance.GetRemainStartCount())
        {
            UIManager.Instance.ShowRetryUI();
            return;
        }

        bool isMove = characterMove.MoveStart(playerInput.GetMoveQueue());

        SetIsMoving(isMove);

        GameManager.Instance.SetRemainStartCount();
    }


    public PlayerInput GetPlayerInput()
    {
        return playerInput;
    }

    public void SetIsMoving(bool b)
    {
        isMoving = b;
        GetComponent<Animator>().SetBool("IsMove", b);
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }
}
