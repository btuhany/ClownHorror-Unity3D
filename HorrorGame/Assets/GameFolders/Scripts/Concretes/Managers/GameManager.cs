using AI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : SingletonMonoObject<GameManager> 
{

    public int CompletedClownEvents;
    public event System.Action OnGameOver;
    private void Awake()
    {
        SingletonThisObject(this);
    
    }
    private void OnEnable()
    {
        ClownEventManager.Instance.OnEventCompleted += HandleOnEventCompleted;
    }
    private void HandleOnEventCompleted()
    {
        CompletedClownEvents++;
        if(CompletedClownEvents == 6)
        {
            GameCompleted();
        }
    }
    private void GameCompleted()
    {
        Time.timeScale = 0f;
    }
    public void GameOver()
    {
        Time.timeScale = 0.5f;
        OnGameOver?.Invoke();
    }
    public void ClownEvent()
    {
        ClownEventManager.Instance.StartEvent();
    }




}
