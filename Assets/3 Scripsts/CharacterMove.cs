using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;

    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private float bumpThreshold = 0.01f;

    private Character character;

    private List<MoveData> moveQueue;
    private MoveData curMoveData;

    private BoxCollider2D boxCollider;
    private float moveStartTime;
    private int queueIndex = 0;
    private bool isBump = false;

    private void Awake()
    {
        character = GetComponent<Character>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (!character.GetIsMoving())
            return;

        // 현재 MoveData의 시간이 끝나면 다음 큐로 진행.
        if (curMoveData.time <= Time.time - moveStartTime)
        {
            ProcessQueue();
        }


        // 충돌이 없으면 이동.
        if (!isBump)
        {
            Vector3 moveDelta = curMoveData.direction * moveSpeed * Time.deltaTime;
            
            if (CanMove(moveDelta, curMoveData.direction))
            {
                transform.position += moveDelta;
            }
            else
            {
                isBump = true;
            }
        }
    }

    private void ProcessQueue()
    {
        queueIndex++;

        if (queueIndex >= moveQueue.Count)
        {
            character.SetIsMoving(false);
            MoveEnd();
            return;
        }

        curMoveData = moveQueue[queueIndex];
        moveStartTime = Time.time;

        isBump = false;
    }

    private bool CanMove(Vector3 moveDelta, Vector3 direction)
    {
        Vector2 origin = (Vector2)transform.position;
        float distance = moveDelta.magnitude + bumpThreshold;

        Vector2 boxSize = boxCollider.size * transform.lossyScale;

        RaycastHit2D hit = Physics2D.BoxCast(
            origin,
            boxSize,
            0f, 
            direction,
            distance,
            obstacleMask
        );

        if (hit.collider != null)
        {
            return false;
        }

        return true;
    }


    public bool MoveStart(List<MoveData> moveList)
    {
        moveQueue = moveList;
        if (moveQueue.Count < 1)
        {
            return false;
        }

        moveStartTime = Time.time;
        curMoveData = moveQueue[0];

        return true;
    }

    public void MoveEnd()
    {
        isBump = false;
        queueIndex = 0;
        moveQueue.Clear();
        character.MoveEnd();
    }

    public void PushCharacter(Vector3 pos, Vector3 direction, float speed)
    {
        character.SetIsMoving(false);
        MoveEnd();
        character.ResetPosition();
    }
}
