using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    public float maxValue = 10f;
    private float targetValue = 10f;
    private float displayValue = 3f;
    [SerializeField] private float smoothSpeed = 5f;

    private void Start()
    {
        GameManager.Instance.OnRemainTimeChanged += UpdateRemainTime;

        maxValue = GameManager.Instance.GetMaxMoveTime();
        targetValue = maxValue;
    }

    private void Update()
    {
        displayValue = Mathf.Lerp(displayValue, targetValue, Time.deltaTime * smoothSpeed);
        fillImage.fillAmount = displayValue / maxValue;
    }

    public void UpdateRemainTime(float remainTime)
    {
        targetValue = Mathf.Clamp(remainTime, 0, maxValue);
    }
}
