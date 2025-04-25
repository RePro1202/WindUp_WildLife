using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;

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
        if (character.GetIsMoving())
        {
            if (curMoveData.time <= Time.time - moveStartTime)
            {
                queueIndex++;

                if (queueIndex >= moveQueue.Count)
                {
                    character.SetIsMoving(false);
                    MoveEnd();
                    return;
                }

                curMoveData = moveQueue[queueIndex];
                isBump = false;
                moveStartTime = Time.time;
            }

            // 충돌하면 제자리에서 걷는 모션만 나오고 이동은 없음.
            if (!isBump)
            {
                Vector3 moveDelta = curMoveData.direction * moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDelta, 0.1f);
                BumpHandle(curMoveData.direction);
            }
        }
    }


    // 진행하던 방향에 따라 일정 값을 빼고 0.5단위로 반올림해서 위치 조정.
    private void BumpHandle(Vector3 direction)
    {
        List<Collider2D> collider = new List<Collider2D>();
        boxCollider.Overlap(collider);

        if (collider.Count > 0)
        {
            if (collider[0].tag == "Obstacle")
            {
                isBump = true;

                //Debug.Log($"충돌 위치:{transform.position}, 충돌 방향: {direction}");

                Vector3 curPos = transform.position;
                Vector3 target = new Vector3();

                if (direction == Vector3.up)
                {
                    curPos.y -= 0.26f;

                    target = RoundVector3ToStep(curPos, 0.5f);
                    target.x = transform.position.x;
                }
                else if(direction == Vector3.down)
                {
                    curPos.y += 0.26f;

                    target = RoundVector3ToStep(curPos, 0.5f);
                    target.x = transform.position.x;
                }
                else if(direction == Vector3.left)
                {
                    curPos.x += 0.26f;

                    target = RoundVector3ToStep(curPos, 0.5f);
                    target.y = transform.position.y;
                }
                else if(direction == Vector3.right)
                {
                    curPos.x -= 0.26f;

                    target = RoundVector3ToStep(curPos, 0.5f);
                    target.y = transform.position.y;
                }

                transform.position = target;

                //Debug.Log($"{collider.Count}");
            }
        }
        else
        {
            isBump = false;
        }
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

    private Vector3 RoundVector3ToStep(Vector3 input, float step)
    {
        float x = Mathf.Round(input.x / step) * step;
        float y = Mathf.Round(input.y / step) * step;
        float z = Mathf.Round(input.z / step) * step;
        return new Vector3(x, y, z);
    }

    public void MoveEnd()
    {
        BumpHandle(curMoveData.direction);
        isBump = false;
        queueIndex = 0;
        moveQueue.Clear();
        character.MoveEnd();
    }
}
