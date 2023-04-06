using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCompletedPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _clownBoxInfoText;
    [SerializeField] RectTransform _youWinText;
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
        ClownBoxInfoText();
        YouDiedTextAnim();
    }
    private void ClownBoxInfoText()
    {
        _clownBoxInfoText.DOFade(0, 0f);
      
        _clownBoxInfoText.DOFade(1, 2f);
    }
    private void YouDiedTextAnim()
    {
        _youWinText.localScale = new Vector3(1, 0, 1);
        _youWinText.DOScaleY(1, 2f);


    }

}
