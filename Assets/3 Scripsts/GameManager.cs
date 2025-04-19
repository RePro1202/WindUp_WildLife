using System;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private float maxMoveTime = 10f;
    [SerializeField] private bool timeOut = false;
    [SerializeField] private int startCount = 4;

    private float remainMoveTime;
    public event Action<float> OnRemainTimeChanged;

    private int remainStartCount;
    public event Action<int> OnRemainStartCount;

    private Character character;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        remainStartCount = startCount;
        remainMoveTime = maxMoveTime;
    }

    private void Start()
    {
        character = FindFirstObjectByType<Character>();
    }


    public void SetRemainMoveTime(float time)
    {
        remainMoveTime = Mathf.Round( (remainMoveTime - time) * 100) / 100;

        if(remainMoveTime <= 0.001f)
        {
            timeOut = true;
            remainMoveTime = 0;
        }

        OnRemainTimeChanged?.Invoke(remainMoveTime);
    }

    public void SetRemainStartCount()
    {
        remainStartCount -= 1;
        OnRemainStartCount?.Invoke(remainStartCount);
    }

    public float GetMaxMoveTime()
    {
        return maxMoveTime;
    }

    public float GetRemainMoveTime()
    {
        return remainMoveTime;
    }

    public Character GetCharacter()
    {
        return character;
    }

    public bool GetTimeOut()
    {
        return timeOut;
    }

    public int GetRemainStartCount()
    {
        return remainStartCount;
    }
}
