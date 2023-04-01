using Abstracts;
using DG.Tweening;
using UnityEngine;

public class DoorController : Interactable
{
    [SerializeField] private CollectableID _requirementItem;
    bool _isOpened;
    public override void Interact()
    {
        if(PlayerInventoryManager.Instance.IsInInventory(_requirementItem))
        {
            OpenOrClose();

        }
    }
    private void OpenOrClose()
    {
        if(_isOpened)
        {
            transform.DOLocalRotate(new Vector3(0, 0, 0), 1);
            _isOpened= false;
        }
        else
        {
            transform.DOLocalRotate(new Vector3(0, 90, 0), 1);
            _isOpened = true;
        }
    }
}
