using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClownBoxTextUpdater : MonoBehaviour
{
    TextMeshProUGUI _text;
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        GameManager.Instance.OnCompletedClownIncreased += HandleOnClownBoxIncreased;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnCompletedClownIncreased -= HandleOnClownBoxIncreased;
    }
    void HandleOnClownBoxIncreased()
    {
        _text.SetText( GameManager.Instance.CompletedClownEvents + "/6 box burned" );
    }
}
