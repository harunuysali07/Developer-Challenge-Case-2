using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public UIStart startUI;
    public UIGamePlay gamePlayUI;
    public UIEndGame endGameUI;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        startUI.gameObject.SetActive(true);
        gamePlayUI.gameObject.SetActive(false);
        endGameUI.gameObject.SetActive(false);

        GameController.Instance.OnLevelStart += OnLevelStart;
        GameController.Instance.OnLevelEnd += OnLevelEnd;
    }

    public void OnLevelStart()
    {
        startUI.gameObject.SetActive(false);
        gamePlayUI.gameObject.SetActive(true);
    }

    public void OnLevelEnd(bool isWin)
    {
        gamePlayUI.gameObject.SetActive(false);
        endGameUI.gameObject.SetActive(true);

        endGameUI.ShowEndGame(isWin);

        DataManager.PlayerLevel += isWin ? 1 : 0;
    }
}
