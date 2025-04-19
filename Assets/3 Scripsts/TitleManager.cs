using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public static TitleManager Instance;

    public bool isGameActive = false;
    public bool isGameOver = false;
    public bool isStageClear = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� �ٲ� ����
        }
        else
        {
            Destroy(gameObject);
        }
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

        if (isStageClear)
        {
            NextStage();
        }
    }

    public void StartGame()
    {
        isGameActive = true;
        isGameOver = false;
        isStageClear = false;
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

    public void StageClear()
    {
        isStageClear = true;
        Debug.Log("�������� Ŭ����!");
        // �������� Ŭ����
    }

    public void RestartGame()
    {
        Debug.Log("���� �����!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextStage()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}