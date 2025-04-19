using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    private bool isOverlapping = false;
    private float overlapTime = 0f;
    public float activationDelay = 0.5f;
    public bool isActivated = false;

    public Sprite inactiveSprite; 
    public Sprite activeSprite;

    void Update()
    {
        if (isOverlapping && !isActivated)
        {
            overlapTime += Time.deltaTime;

            if (overlapTime >= activationDelay)
            {
                ActivateButton();
            }
        }
        else
        {
            // �ð��� ���µǴ� ����
            overlapTime = 0f;
        }
    }

    private void ActivateButton()
    {
        isActivated = true;
        Debug.Log("��ư�� �������ϴ�!");
        // ���⿡ ��ư �۵� ���� �߰�
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ȥ�� ������ �±׷� üũ
        {
            isOverlapping = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isOverlapping = false;
        }
    }
}