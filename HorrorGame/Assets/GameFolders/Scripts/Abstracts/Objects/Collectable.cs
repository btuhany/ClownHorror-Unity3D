
using Abstracts;

namespace Abstracts
{
    public abstract class Collectable : Interactable
    {
        public CollectableID CollectableID;
        public override void Interact()
        {
            Collect();
        }
        protected virtual void Collect()
        {
            PlayerInventoryManager.Instance.AddToList(this);
            Destroy(this.gameObject);
        }

    }

}
