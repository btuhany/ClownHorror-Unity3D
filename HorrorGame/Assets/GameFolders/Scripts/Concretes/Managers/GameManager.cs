
using System.Collections;

using UnityEngine;

using UnityEngine.SceneManagement;




public class GameManager : SingletonMonoObject<GameManager> 
{
    bool _isInGame;
    bool _isGamePaused;
    public int CompletedClownEvents;

    public bool IsGamePaused { get => _isGamePaused; }

    public event System.Action OnGameUnpaused;
    public event System.Action OnGamePaused;
    public event System.Action OnGameOver;
    public event System.Action OnGameStarted;
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
        _isInGame = false;
        SoundManager.Instance.StopAllSounds();
        SoundManager.Instance.PlaySoundFromSingleSource(10);
        OnGameCompleted?.Invoke();
        StartCoroutine(StopTimescaleWithDelay());
    }
    public void GameOver()
    {
        _isInGame = false;
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
    public void PauseResumeGame()
    {
        if(_isInGame)
        {
         
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale= 0f;
            SoundManager.Instance.PauseAllSounds();
            OnGamePaused?.Invoke();
            _isGamePaused = true;
            _isInGame= false;
        }
        else if(_isGamePaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            SoundManager.Instance.UnpauseAllSounds();
            OnGameUnpaused?.Invoke();
            _isInGame = true;
            _isGamePaused = false;
        }
       
       
    }
    public void RestartGame()
    {
        _isInGame = true;
        StopAllCoroutines();
        OnGameRestart?.Invoke();
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneFromIndexAsync(0));
    }
    public void StartGame()
    {
        OnGameStarted?.Invoke();
        _isInGame = true;
        SoundManager.Instance.StopAllSounds();
        SoundManager.Instance.PlaySoundFromSingleSource(5);
        StartCoroutine(LoadSceneFromIndexAsync(1));
        ClownEventManager.Instance.GameStarted();
      
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
