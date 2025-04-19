using UnityEngine;

public class Car1Behaviour : MonoBehaviour
{
    public float currentPosition = 0f;  // ���� ��ġ (�����/���ؿ�)
    public float minPosition = -3f;     // ���� ��ġ
    public float maxPosition = 3f;      // �ְ� ��ġ
    public float speed = 3f;            // �̵� �ӵ�

    public int direction = 1;          // �̵� ����: 1 = ������, -1 = ����

    void Update()
    {
        // �̵�
        float movement = speed * Time.deltaTime * direction;
        transform.position += new Vector3(movement, 0f, 0f);

        // ���� ��ġ ����
        currentPosition = transform.position.x;

        // ���� ���� ����
        if (currentPosition >= maxPosition || currentPosition <= minPosition)
        {
            if (direction == 1)
            {
                transform.position += new Vector3(-maxPosition + minPosition, 0f, 0f);
                currentPosition = minPosition;
            }  
            else
            {
                transform.position += new Vector3(maxPosition - minPosition, 0f, 0f);
                currentPosition = maxPosition;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Game over");
        TitleManager.Instance.GameOver();
    }
}
