using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private List<MoveData> moveList = new List<MoveData>();

    private Character character;

    private KeyCode? currentKey = null;
    private float keyDownTime = 0f;       
    private bool isPressed = false;
    private float heldTime = 0f;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    void Update()
    {
        if(isPressed && currentKey.HasValue)
        {
            heldTime = Time.time - keyDownTime;
            UIManager.Instance.UpdateLastArrowTime(heldTime);

            if (Input.GetKeyUp(currentKey.Value))
            {
                Debug.Log($"{currentKey} 눌린 시간: {heldTime:F2}초");

                if (currentKey.Value == KeyCode.W)
                {
                    moveList.Add(new MoveData(Vector2.up, heldTime));
                }
                else if (currentKey.Value == KeyCode.A)
                {
                    moveList.Add(new MoveData(Vector2.left, heldTime));
                } 
                else if (currentKey.Value == KeyCode.S)
                {
                    moveList.Add(new MoveData(Vector2.down, heldTime));
                }
                else if (currentKey.Value == KeyCode.D)
                {
                    moveList.Add(new MoveData(Vector2.right, heldTime));
                }
                    
                updateRemainTime(heldTime);

                isPressed = false;
                currentKey = null;
            }
        }
        else if(!(GameManager.Instance.GetTimeOut()))
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                PressKey(KeyCode.W);
                UIManager.Instance.SpawnArrow(EKeyType.W, heldTime);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                PressKey(KeyCode.A);
                UIManager.Instance.SpawnArrow(EKeyType.A, heldTime);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                PressKey(KeyCode.S);
                UIManager.Instance.SpawnArrow(EKeyType.S, heldTime);
            } 
            else if (Input.GetKeyDown(KeyCode.D))
            {
                PressKey(KeyCode.D);
                UIManager.Instance.SpawnArrow(EKeyType.D, heldTime);
            }
        }
    }

    private void PressKey(KeyCode key)
    {
        currentKey = key;
        keyDownTime = Time.time;
        isPressed = true;
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
