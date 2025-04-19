using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public ButtonBehavior[] buttons; // ���� ��ư ����
    public bool IsOpen;

    public Sprite CloseSprite;
    public Sprite OpenSprite;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        IsOpen = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = CloseSprite;
    }

    void Update()
    {
        if (AllButtonsActivated())
        {
            // ���� ��ġ�� �̵�
            IsOpen = true;
            spriteRenderer.sprite = OpenSprite;
        }
        else
        {
            // ���� ��ġ�� �̵�
            IsOpen = false;
            spriteRenderer.sprite = CloseSprite;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsOpen) 
            Debug.Log("Game Clear");
    }

    // ��� ��ư�� ���ȴ��� Ȯ���ϴ� �Լ�
    bool AllButtonsActivated()
    {
        foreach (ButtonBehavior button in buttons)
        {
            if (button == null || !button.isActivated)
                return false;
        }
        return true;
    }
}