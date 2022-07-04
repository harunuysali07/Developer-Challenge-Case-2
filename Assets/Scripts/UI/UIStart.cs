using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStart : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerLevelText;

    void Start()
    {
        playerLevelText.text = "Level " + (DataManager.PlayerLevel + 1);
    }

    public void OnStartButtonClick()
    {
        GameController.Instance.LevelStart();
    }
}
