using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public enum EKeyType { W,A,S,D}
public class UIManager : MonoBehaviour
{
    [Tooltip("W,A,S,D 순서대로")]
    [SerializeField] Image[] arrowImage;

    [SerializeField] Transform arrowPos;
    EKeyType keyType;

    public static UIManager Instance;

    List<Image> curArrowList = new List<Image>();

    [SerializeField] Image fadeImage;
    [SerializeField] GameObject retryUI;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(FadeInAndOut(true));
    }

    public void SpawnArrow(EKeyType eKeyType, float heldTime)
    {
        Image arrow = Instantiate(arrowImage[(int)eKeyType], arrowPos);
        curArrowList.Add(arrow);

        switch (eKeyType)
        {
            case EKeyType.W:
                arrow.GetComponentInChildren<TextMeshProUGUI>().text = heldTime.ToString("F2");
                break;
            case EKeyType.A:
                arrow.GetComponentInChildren<TextMeshProUGUI>().text = heldTime.ToString("F2");
                break;
            case EKeyType.S:
                arrow.GetComponentInChildren<TextMeshProUGUI>().text = heldTime.ToString("F2");
                break;
            case EKeyType.D:
                arrow.GetComponentInChildren<TextMeshProUGUI>().text = heldTime.ToString("F2");
                break;
        }
    }

    public void UpdateLastArrowTime(float heldTime)
    {
        if(curArrowList.Count <= 0) 
        { 
            return; 
        }

        curArrowList[curArrowList.Count - 1].GetComponentInChildren<TextMeshProUGUI>().text = heldTime.ToString("F2");
    }

    public void ClearArrow()
    {
        foreach(var arrow in curArrowList)
        {
            Destroy(arrow.gameObject);
        }

        curArrowList.Clear();
    }

    public IEnumerator FadeInAndOut(bool isOpen)
    {
        if (isOpen)
        {
            Color color = fadeImage.color;
            color.a = 1f;

            while (color.a > 0f)
            {
                color.a -= Time.deltaTime;
                fadeImage.color = color;
                yield return null;
            }
        }
        else
        {
            Color color = fadeImage.color;
            color.a = 0f;

            while (color.a < 1f)
            {
                color.a += Time.deltaTime;
                fadeImage.color = color;
                yield return null;
            }

            // 씬 전환 로직
        }
       
    }

    public void ShowRetryUI()
    {
        retryUI.SetActive(true);
    }

    public void OnClickRetryBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
