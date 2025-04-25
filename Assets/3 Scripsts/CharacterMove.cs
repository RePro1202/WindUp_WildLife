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

            if (!isBump)
            {
                Vector3 moveDelta = curMoveData.direction * moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDelta, 0.1f);
            }

            BumpHandle();
        }
    }

    private void BumpHandle()
    {
        List<Collider2D> coll = new List<Collider2D>();
        boxCollider.Overlap(coll);

        if (coll.Count > 0)
        {
            if (coll[0].tag == "Obstacle")
            {
                isBump = true;

                Debug.Log($"충돌 위치{transform.position}");
                transform.position = RoundVector3ToStep(transform.position, 0.5f);
            }
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
        isBump = false;
        queueIndex = 0;
        moveQueue.Clear();
        character.MoveEnd();
    }
}
