
using Abstracts;

public abstract class Collectable : Interactable
{
    public override void Interact()
    {
        Collect();
    }
    protected virtual void Collect()
    {
        Destroy(gameObject);

    }

}
