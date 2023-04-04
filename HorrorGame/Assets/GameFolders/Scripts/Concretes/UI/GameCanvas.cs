using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] GameObject _gameCompletedPanel;
    [SerializeField] GameObject _inGamePanel;
    private void OnEnable()
    {
        GameManager.Instance.OnGameOver += HandleOnGameOver;
        GameManager.Instance.OnGameCompleted += HandleOnGameCompleted;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGameOver -= HandleOnGameOver;
        GameManager.Instance.OnGameCompleted -= HandleOnGameCompleted;
    }
    private void HandleOnGameOver()
    {
        _gameOverPanel.SetActive(true);
        _inGamePanel.SetActive(false);
    }
    private void HandleOnGameCompleted()
    {
        _gameCompletedPanel.SetActive(true);
        _inGamePanel.SetActive(false);
    }
   
}
