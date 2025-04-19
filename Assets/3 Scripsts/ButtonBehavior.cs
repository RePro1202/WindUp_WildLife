using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    private bool isOverlapping = false;
    private float overlapTime = 0f;
    public float activationDelay = 0.5f;
    public bool isActivated = false;

    public Sprite inactiveSprite; 
    public Sprite activeSprite;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = inactiveSprite;
    }

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
            // 시간이 리셋되는 조건
            overlapTime = 0f;
        }
    }

    private void ActivateButton()
    {
        isActivated = true;
        spriteRenderer.sprite = activeSprite;
        Debug.Log("버튼이 켜졌습니다!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("버튼 누르는 중");
        if (other.CompareTag("Player")) // 혹은 적절한 태그로 체크
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