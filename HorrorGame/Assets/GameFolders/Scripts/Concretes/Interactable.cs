using Abstracts;
using UnityEngine;

namespace Abstracts
{
    public abstract class Interactable : Targetable
    {
        public GameObject Object => this.gameObject;
        public virtual void Interact()
        {

        }



    }
}
