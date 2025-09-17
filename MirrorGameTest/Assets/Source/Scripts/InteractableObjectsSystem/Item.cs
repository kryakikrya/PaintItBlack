using UnityEngine;

public abstract class Item : InteractableObject
{
    public override void OnServerInteract(GameObject player)
    {
        player.GetComponent<PlayerInventory>().TryPickUp(this);
    }

    public abstract void Use(GameObject player);
}
