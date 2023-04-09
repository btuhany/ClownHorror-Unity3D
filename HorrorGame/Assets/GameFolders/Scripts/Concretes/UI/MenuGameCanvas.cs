using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGameCanvas : MonoBehaviour
{
    [SerializeField] GameObject _menuPanel;
    [SerializeField] GameObject _loadingPanel;

    private void OnEnable()
    {
        GameManager.Instance.OnGameStarted += HandleOnGameStarted;

    }
    private void OnDisable()
    {
        GameManager.Instance.OnGameStarted -= HandleOnGameStarted;

    }

    private void HandleOnGameStarted()
    {
        _menuPanel.SetActive(false);
        _loadingPanel.SetActive(true);
    }
}
