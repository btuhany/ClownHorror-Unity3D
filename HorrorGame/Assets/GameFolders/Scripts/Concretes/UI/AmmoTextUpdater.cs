
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoTextUpdater : MonoBehaviour
{
    TextMeshProUGUI _text;
    private void Awake()
    {
        _text= GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        PlayerInventoryManager.Instance.OnAmmoChanged += HandleOnHealthChanged;
    }
    void HandleOnHealthChanged()
    {
        _text.SetText("Ammo: " + PlayerInventoryManager.Instance.TotalAmmo);
    }
}
