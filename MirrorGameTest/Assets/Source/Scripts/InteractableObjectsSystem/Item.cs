using Mirror;
using UnityEngine;

public abstract class Item : InteractableObject
{

    [Server]
    public override void OnServerInteract(GameObject player)
    {
        print("OnServerInteract");
        if (player.TryGetComponent(out PlayerInventory inventory))
        {
            inventory.PickUp(this);
        }
        NetworkServer.Destroy(gameObject);
    }

    public abstract void Use(GameObject player);
}
