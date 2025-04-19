using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public ButtonBehavior[] buttons; // 여러 버튼 참조
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
            // 열림 위치로 이동
            IsOpen = true;
            spriteRenderer.sprite = OpenSprite;
        }
        else
        {
            // 닫힘 위치로 이동
            IsOpen = false;
            spriteRenderer.sprite = CloseSprite;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsOpen) 
            Debug.Log("Game Clear");
    }

    // 모든 버튼이 눌렸는지 확인하는 함수
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