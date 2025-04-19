using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MoveCountUI : MonoBehaviour
{
    TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.Instance.OnRemainTimeChanged += UpdateRemainTime;
        textMeshPro.text = $"{GameManager.Instance.GetRemainMoveTime()}";
    }

    public void UpdateRemainTime(float remainTime)
    {
        textMeshPro.text = remainTime.ToString("F2");
    }
}
