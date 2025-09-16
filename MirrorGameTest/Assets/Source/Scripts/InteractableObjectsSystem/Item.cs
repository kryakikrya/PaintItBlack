using UnityEngine;

public abstract class Item : InteractableObject
{
    public override void Interact(GameObject player)
    {
        // to do: take the item
    }

    public virtual void Use() { }

    public virtual void Throw() { }
}
