using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject inGameCamera;
    public GameObject endGameCamera;

    public void Start()
    {
        GameController.Instance.OnLevelStart += OnLevelStart;
        GameController.Instance.OnLevelEnd += OnLevelEnd;
    }

    private void OnLevelStart()
    {
        inGameCamera.SetActive(true);
        endGameCamera.SetActive(false);
    }

    private void OnLevelEnd(bool isWin)
    {
        inGameCamera.SetActive(false);
        endGameCamera.SetActive(true);
    }

    private void Update()
    {
        
    }
}
