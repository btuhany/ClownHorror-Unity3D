using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    Slider _slider;
    private void Awake()
    {
        _slider= GetComponent<Slider>();
        _slider.maxValue = 6.5f;
    }
    public void SetSlider(float value)
    {
        
        _slider.value = value;
    }

}
