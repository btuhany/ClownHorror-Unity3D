using AI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoObject<GameManager> 
{

    public int CompletedClownEvents;
    public event System.Action OnGameOver;
    public event System.Action OnGameCompleted;
    public event System.Action OnGameRestart;
    public event System.Action OnNormalDiff;
    public event System.Action OnHardDiff;
    public event System.Action OnCompletedClownIncreased;
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
        OnCompletedClownIncreased?.Invoke();
        if (CompletedClownEvents == 6)
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
    public void GameCompleted()
    {
        SoundManager.Instance.StopAllSounds();
        SoundManager.Instance.PlaySoundFromSingleSource(10);
        OnGameCompleted?.Invoke();
        StartCoroutine(StopTimescaleWithDelay());
    }
    public void GameOver()
    {
        SoundManager.Instance.StopAllSounds();
        SoundManager.Instance.PlaySoundFromSingleSource(9);
        OnGameOver?.Invoke();
        StartCoroutine(StopTimescaleWithDelay());
    }
    public void ClownEvent()
    {
        SoundManager.Instance.PlaySoundFromSingleSource(8);
        SoundManager.Instance.PlaySoundFromSingleSource(6);
        ClownEventManager.Instance.StartEvent();

    }
    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void RestartGame()
    {
        StopAllCoroutines();
        OnGameRestart?.Invoke();
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneFromIndexAsync(0));
    }
    private IEnumerator LoadSceneFromIndexAsync(int sceneIndex)
    {
        yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + sceneIndex);
    }
    IEnumerator StopTimescaleWithDelay()
    {
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0f;
        yield return null;
    }


}
