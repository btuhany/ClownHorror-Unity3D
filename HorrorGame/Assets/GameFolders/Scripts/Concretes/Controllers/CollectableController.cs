using Abstracts;
using System;
using UnityEngine;

namespace Controllers
{
    public class CollectableController : Collectable
    {
      
        public override void Interact()
        {
            PlayerInventoryManager.Instance.AddToList(this);
            base.Interact();
        }



    }

}
