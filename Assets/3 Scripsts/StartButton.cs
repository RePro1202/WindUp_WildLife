using TMPro;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;

    private void Start()
    {
        GameManager.Instance.OnRemainStartCount += UpdateRemainCount;

        textMeshPro.text = "Start " + GameManager.Instance.GetRemainStartCount();
    }

    public void UpdateRemainCount(int t)
    {
        textMeshPro.text = "Start " + t;
    }
}
