using UnityEngine;

public class Car1Behaviour : MonoBehaviour
{
    public float currentPosition = 0f;  // ���� ��ġ (�����/���ؿ�)
    public float minPosition = -3f;     // ���� ��ġ
    public float maxPosition = 3f;      // �ְ� ��ġ
    public float speed = 3f;            // �̵� �ӵ�

    public int direction = 1;          // �̵� ����: 1 = ������, -1 = ����

    private Vector3 pushDir = Vector3.zero;

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

                pushDir = Vector3.right;
            }  
            else
            {
                transform.position += new Vector3(maxPosition - minPosition, 0f, 0f);
                currentPosition = maxPosition;

                pushDir = Vector3.left;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<CharacterMove>(out var characterMove))
        {
            characterMove.PushCharacter(transform.position, pushDir, speed);
        }
    }
}
