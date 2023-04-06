
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSliderController : MonoBehaviour
{
    Slider _slider;
    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = 18f;
    }
    public void SetSlider(float value)
    {

        _slider.value = value;
    }
}
