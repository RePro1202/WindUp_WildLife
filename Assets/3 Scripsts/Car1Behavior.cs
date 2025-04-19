using UnityEngine;

public class Car1Behaviour : MonoBehaviour
{
    public float currentPosition = 0f;  // 현재 위치 (디버깅/기준용)
    public float minPosition = -3f;     // 최저 위치
    public float maxPosition = 3f;      // 최고 위치
    public float speed = 3f;            // 이동 속도

    public int direction = 1;          // 이동 방향: 1 = 오른쪽, -1 = 왼쪽

    void Update()
    {
        // 이동
        float movement = speed * Time.deltaTime * direction;
        transform.position += new Vector3(0f, movement, 0f);

        // 현재 위치 갱신
        currentPosition = transform.position.y;

        // 방향 반전 조건
        if (currentPosition >= maxPosition || currentPosition <= minPosition)
        {
            if (direction == 1)
            {
                transform.position += new Vector3(0f, - maxPosition + minPosition, 0f);
                currentPosition = minPosition;
            }  
            else
            {
                transform.position += new Vector3(0f, maxPosition - minPosition, 0f);
                currentPosition = maxPosition;
            }
        }
    }
}
