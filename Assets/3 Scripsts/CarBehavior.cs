using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    public float currentPosition = 0f;  // ���� ��ġ (�����/���ؿ�)
    public float minPosition = -3f;     // ���� ��ġ
    public float maxPosition = 3f;      // �ְ� ��ġ
    public float speed = 3f;            // �̵� �ӵ�

    private int direction = 1;          // �̵� ����: 1 = ������, -1 = ����

    private Vector3 pushDir = Vector3.zero;

    void Update()
    {
        Movement();
    }

    private void Movement()
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
            pushDir = Vector3.down;
        }
        else if (currentPosition <= minPosition)
        {
            direction = 1;
            pushDir = Vector3.up;
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
