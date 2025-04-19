using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    [SerializeField] private Image topImage;
    [SerializeField] private Image middleImage;

    public float maxValue = 10f;
    private float targetValue = 10f;
    private float displayValue = 3f;
    [SerializeField] private float smoothSpeed = 5f;

    [SerializeField] private PlayerInput playerInput;

    private void Start()
    {
        GameManager.Instance.OnRemainTimeChanged += UpdateRemainTime;

        maxValue = GameManager.Instance.GetMaxMoveTime();
        targetValue = maxValue;
        //playerInput = GameManager.Instance.GetCharacter().GetPlayerInput();
    }

    private void Update()
    {
        displayValue = Mathf.Lerp(displayValue, targetValue, Time.deltaTime * smoothSpeed);
        middleImage.fillAmount = displayValue / maxValue;

        topImage.fillAmount = (targetValue - (playerInput.GetCurHeldTime() * Time.deltaTime)) / maxValue;
    }

    public void UpdateRemainTime(float remainTime)
    {
        targetValue = Mathf.Clamp(remainTime, 0, maxValue);
    }
}
