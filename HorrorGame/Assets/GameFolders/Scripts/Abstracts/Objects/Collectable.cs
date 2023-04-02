
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
            
            Destroy(this.gameObject,0.1f);
        }

    }

}
