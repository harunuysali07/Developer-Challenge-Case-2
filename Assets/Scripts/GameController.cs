using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public static bool GameState = false;

    public DifficultyData DifficultyData;

    public UnityAction OnLevelStart;
    public UnityAction<bool> OnLevelEnd;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public void LevelStart()
    {
        GameState = true;

        OnLevelStart?.Invoke();
    }

    public void LevelFailed()
    {
        GameState = false;

        OnLevelEnd?.Invoke(false);
    }

    public void LevelCompleted()
    {
        GameState = false;

        OnLevelEnd?.Invoke(true);
    }
}
