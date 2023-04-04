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
    public event System.Action OnNormalDiff;
    public event System.Action OnHardDiff;
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
            return;
        }
        else if(CompletedClownEvents == 4)
        {
            OnHardDiff?.Invoke();
        }
        else if(CompletedClownEvents == 2)
        {
            OnNormalDiff?.Invoke();
        }
        SoundManager.Instance.PlaySoundFromSingleSource(7);
        SoundManager.Instance.StopSoundSource(8);
    }
    private void GameCompleted()
    {
        Time.timeScale = 0f;
    }
    public void GameOver()
    {
        Time.timeScale = 0.2f;
        OnGameOver?.Invoke();
    }
    public void ClownEvent()
    {
        SoundManager.Instance.PlaySoundFromSingleSource(8);
        SoundManager.Instance.PlaySoundFromSingleSource(6);
        ClownEventManager.Instance.StartEvent();

    }




}
