using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;

public enum EKeyType { W,A,S,D}
public class UIManager : MonoBehaviour
{
    [Tooltip("W,A,S,D 순서대로")]
    [SerializeField] Image[] arrowImage;

    [SerializeField] Transform arrowPos;
    EKeyType keyType;

    
    public void SpawnArrow(EKeyType eKeyType, float heldTime)
    {
        Image arrow = Instantiate(arrowImage[(int)eKeyType], arrowPos);

        switch (eKeyType)
        {
            case EKeyType.W:
                arrow.GetComponentInChildren<TextMeshProUGUI>().text = heldTime.ToString();
                break;
            case EKeyType.A:
                arrow.GetComponentInChildren<TextMeshProUGUI>().text = heldTime.ToString();
                break;
            case EKeyType.S:
                arrow.GetComponentInChildren<TextMeshProUGUI>().text = heldTime.ToString();
                break;
            case EKeyType.D:
                arrow.GetComponentInChildren<TextMeshProUGUI>().text = heldTime.ToString();
                break;
        }
    }
}
