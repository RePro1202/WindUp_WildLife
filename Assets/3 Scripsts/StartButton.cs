using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;

    Character character;

    private void Start()
    {
        GameManager.Instance.OnRemainStartCount += UpdateRemainCount;

        GetComponent<Button>().onClick.AddListener(ButtonClicked);

        textMeshPro.text = "Start " + GameManager.Instance.GetRemainStartCount();
    }

    public void UpdateRemainCount(int t)
    {
        textMeshPro.text = "Start " + t;
    }
    
    private void ButtonClicked()
    {
        GameManager.Instance.GetCharacter().MoveStart();
    }
   
}
