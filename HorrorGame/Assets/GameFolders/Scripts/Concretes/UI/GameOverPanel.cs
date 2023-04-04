using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _clownBoxInfoText;
    [SerializeField] RectTransform _youDiedText;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
        ClownBoxInfoText();
        YouDiedTextAnim();
    }
    public void PlayAgain()
    {
        GameManager.Instance.RestartGame();
    }
    public void Quit()
    {
        GameManager.Instance.ExitGame();
    }

    private void ClownBoxInfoText()
    {
        _clownBoxInfoText.DOFade(0, 0f);
        _clownBoxInfoText.SetText("You burned " + GameManager.Instance.CompletedClownEvents.ToString() + " of 6 clown boxes.");
        _clownBoxInfoText.DOFade(1, 2f);
    }
    private void YouDiedTextAnim()
    {
        _youDiedText.localScale = new Vector3(1, 0, 1);
        _youDiedText.DOScaleY(1, 2f);
            
    }
}
