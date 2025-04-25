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

            // �浹�ϸ� ���ڸ����� �ȴ� ��Ǹ� ������ �̵��� ����.
            if (!isBump)
            {
                Vector3 moveDelta = curMoveData.direction * moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDelta, 0.1f);
                BumpHandle(curMoveData.direction);
            }
        }
    }


    // �����ϴ� ���⿡ ���� ���� ���� ���� 0.5������ �ݿø��ؼ� ��ġ ����.
    private void BumpHandle(Vector3 direction)
    {
        List<Collider2D> collider = new List<Collider2D>();
        boxCollider.Overlap(collider);

        if (collider.Count > 0)
        {
            if (collider[0].tag == "Obstacle")
            {
                isBump = true;

                //Debug.Log($"�浹 ��ġ:{transform.position}, �浹 ����: {direction}");

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
