using Abstracts;
using System;
using UnityEngine;

namespace Controllers
{
    public class KeyController : Collectable
    {
      
        public override void Interact()
        {
            PlayerInventoryManager.Instance.AddToList(this);
            base.Interact();
        }



    }

}
