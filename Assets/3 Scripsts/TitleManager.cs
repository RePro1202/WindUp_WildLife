using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleManager : MonoBehaviour
{
    public static TitleManager Instance;

    public bool isGameActive = false;
    public bool isGameOver = false;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Update()
    {
        if (!isGameActive && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

    }

    public void StartGame()
    {
        isGameActive = true;
        isGameOver = false;
        Debug.Log("���� ����!");
        // ���� ���� �� �ʿ��� �ʱ�ȭ ���� �߰�
    }

    public void GameOver()
    {
        isGameActive = false;
        isGameOver = true;
        Debug.Log("���� ����!");
        // ���� ���� UI ���� �� �߰�
    }

    public IEnumerator StageClear()
    {
        StartCoroutine(UIManager.Instance.FadeInAndOut(false));
        yield return new WaitUntil(() => UIManager.Instance.isFinished);
        NextStage();
    }

    public void RestartGame()
    {
        Debug.Log("���� �����!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextStage()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex > 5)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex);
        }

    }
}