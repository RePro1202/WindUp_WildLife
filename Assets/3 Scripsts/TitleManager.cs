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
            DontDestroyOnLoad(gameObject); // 씬 바뀌어도 유지
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
        Debug.Log("게임 시작!");
        // 게임 시작 시 필요한 초기화 로직 추가
    }

    public void GameOver()
    {
        isGameActive = false;
        isGameOver = true;
        Debug.Log("게임 오버!");
        // 게임 오버 UI 띄우기 등 추가
    }

    public void StageClear()
    {
        isStageClear = true;
        Debug.Log("스테이지 클리어!");
        // 스테이지 클리어
    }

    public void RestartGame()
    {
        Debug.Log("게임 재시작!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextStage()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}