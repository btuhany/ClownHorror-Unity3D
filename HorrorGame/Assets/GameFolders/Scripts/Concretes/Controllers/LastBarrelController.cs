using Abstracts;
using UnityEngine;
public class LastBarrelController : Interactable
{
    [SerializeField] GameObject _fire;
    public override void Interact()
    {
        if(PlayerInventoryManager.Instance.IsInInventory(CollectableID.Fuel) && PlayerInventoryManager.Instance.IsInInventory(CollectableID.Firelighter))
        {
            _fire.SetActive(true);
            PlayerInventoryManager.Instance.RemoveFromList(CollectableID.Fuel);
        }
        else if(PlayerInventoryManager.Instance.IsInInventory(CollectableID.Fuel))
        {
            //need firelighter
        }
        else if(PlayerInventoryManager.Instance.IsInInventory(CollectableID.Firelighter))
        {
            //need Fuel
        }
    }
}
