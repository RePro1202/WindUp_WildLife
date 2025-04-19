using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private List<MoveData> moveList = new List<MoveData>();

    private KeyCode? currentKey = null;
    private float keyDownTime = 0f;       
    private bool isPressed = false; 

    void Start()
    {
        //moveList.Add(new MoveData(Vector2.up, 3f));
        //moveList.Add(new MoveData(Vector2.left, 1f));
        //moveList.Add(new MoveData(Vector2.down, 2f));
        //moveList.Add(new MoveData(Vector2.right, 1f));
    }

    void Update()
    {
        if(isPressed)
        {
            if (currentKey.HasValue && Input.GetKeyUp(currentKey.Value))
            {
                float heldTime = Time.time - keyDownTime;
                Debug.Log($"{currentKey} 눌린 시간: {heldTime:F2}초");

                if (currentKey.Value == KeyCode.W)
                    moveList.Add(new MoveData(Vector2.up, heldTime));
                else if (currentKey.Value == KeyCode.A)
                    moveList.Add(new MoveData(Vector2.left, heldTime));
                else if (currentKey.Value == KeyCode.S)
                    moveList.Add(new MoveData(Vector2.down, heldTime));
                else if (currentKey.Value == KeyCode.D)
                    moveList.Add(new MoveData(Vector2.right, heldTime));

                isPressed = false;
                currentKey = null;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
                PressKey(KeyCode.W);
            else if (Input.GetKeyDown(KeyCode.A))
                PressKey(KeyCode.A);
            else if (Input.GetKeyDown(KeyCode.S))
                PressKey(KeyCode.S);
            else if (Input.GetKeyDown(KeyCode.D))
                PressKey(KeyCode.D);
        }
    }

    private void PressKey(KeyCode key)
    {
        currentKey = key;
        keyDownTime = Time.time;
        isPressed = true;
    }

    public List<MoveData> GetMoveQueue()
    {
        return moveList;
    }

    public void ResetMoveQueue()
    {
        moveList.Clear();
    }
}
