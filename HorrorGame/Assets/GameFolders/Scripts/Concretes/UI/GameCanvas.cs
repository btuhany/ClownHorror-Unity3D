using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] GameObject _gameCompletedPanel;
    [SerializeField] GameObject _inGamePanel;
    [SerializeField] GameObject _pausePanel;
    private void OnEnable()
    {
        GameManager.Instance.OnGameOver += HandleOnGameOver;
        GameManager.Instance.OnGameCompleted += HandleOnGameCompleted;
        GameManager.Instance.OnGamePaused += HandleOnGamePaused;
        GameManager.Instance.OnGameUnpaused += HandleOnGameUnpaused;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGameOver -= HandleOnGameOver;
        GameManager.Instance.OnGameCompleted -= HandleOnGameCompleted;
        GameManager.Instance.OnGamePaused -= HandleOnGamePaused;
        GameManager.Instance.OnGameUnpaused -= HandleOnGameUnpaused;
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
    private void HandleOnGamePaused()
    {
        _pausePanel.SetActive(true);

    }

    private void HandleOnGameUnpaused()
    {
        _pausePanel.SetActive(false);
    }

}
