
using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;


public class InfoTextUpdater : MonoBehaviour
{
    TextMeshProUGUI _text;
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        PlayerInventoryManager.Instance.OnItemAcquired += HandleOnItemAcquired;
    }
    void HandleOnItemAcquired()
    {
        CollectableID collectableID = PlayerInventoryManager.Instance.LastChangedItemID;
        switch (collectableID)
        {
            case CollectableID.KeyBlue:
                _text.SetText("Item 'Blue Key' Acquired");
                break;
            case CollectableID.KeyGreen:
                _text.SetText("Item 'Green Key' Acquired");
                break;
            case CollectableID.KeyRed:
                _text.SetText("Item 'Red Key' Acquired");
                break;
            case CollectableID.KeyBlack:
                _text.SetText("Item 'Black Key' Acquired");
                break;
            case CollectableID.Fuel:
                _text.SetText("Item 'Fuel' Acquired. Use it with an unused barrel to make fire");
                break;
            case CollectableID.Firelighter:
                _text.SetText("Item 'Firelighter' Acquired. Use it with an unused barrel to make fire");
                break;
            default:
                break;
        }
        _text.DOFade(1, 2f);
        StartCoroutine(TextFadeOut());
      
        
    }
    IEnumerator TextFadeOut()
    {
        yield return new WaitForSeconds(3.2f);
        _text.DOFade(0, 0.5f);
        yield return null;
    }
}