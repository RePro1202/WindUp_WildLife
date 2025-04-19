using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    public float currentPosition = 0f;  // ���� ��ġ (�����/���ؿ�)
    public float minPosition = -3f;     // ���� ��ġ
    public float maxPosition = 3f;      // �ְ� ��ġ
    public float speed = 3f;            // �̵� �ӵ�

    private int direction = 1;          // �̵� ����: 1 = ������, -1 = ����

    void Update()
    {
        // �̵�
        float movement = speed * Time.deltaTime * direction;
        transform.position += new Vector3(0f, movement, 0f);

        // ���� ��ġ ����
        currentPosition = transform.position.y;

        // ���� ���� ����
        if (currentPosition >= maxPosition)
        {
            direction = -1;
        }
        else if (currentPosition <= minPosition)
        {
            direction = 1;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Game over");
    }
}
