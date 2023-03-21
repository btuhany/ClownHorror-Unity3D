
using Abstracts;
using System.Diagnostics;

public abstract class Collectable : Interactable
{
    public override void Interact()
    {
        Collect();
    }
    protected virtual void Collect()
    {

        Destroy(this.gameObject);
    }

}
