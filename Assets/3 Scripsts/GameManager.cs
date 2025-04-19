using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private float maxMoveTime = 10f;
    [SerializeField] private bool timeOut = false;

    private float remainMoveTime;
    public event Action<float> OnRemainTimeChanged;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        remainMoveTime = maxMoveTime;
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

    public float GetMaxMoveTime()
    {
        return maxMoveTime;
    }

    public float GetRemainMoveTime()
    {
        return remainMoveTime;
    }

    public bool GetTimeOut()
    {
        return timeOut;
    }
}
