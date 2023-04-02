using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePanel : MonoBehaviour
{
    public static InGamePanel Instance { get; set; }

    [SerializeField] RectTransform _crossHair;

    public void HideCrossHair()
    {
        _crossHair.gameObject.SetActive(false);
    }
    public void UnhideCrossHair()
    {
        _crossHair.gameObject.SetActive(true);
    }
}
