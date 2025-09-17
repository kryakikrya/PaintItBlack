using UnityEngine;

public abstract class Item : InteractableObject
{
    public override void OnServerInteract(GameObject player)
    {
        
    }

    public abstract void Use(GameObject player);
}
