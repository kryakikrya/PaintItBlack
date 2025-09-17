using UnityEngine;
using System.Collections.Generic;
using Mirror;
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform _handSocket;
    private NetworkIdentity _handItem;

    private List<Item> _items = new List<Item>(4);

    public void TryPickUp(Item item, int slotId)
    {
        if (_items[slotId] != null)
        {
            DropItemById(slotId);
        }
        _items[slotId] = item;
        // to do: swap item in hand here
    }

    public void DropItemById(int id)
    {
        // to do: throw away an item
    }

    public Item GetItemById(int id) => _items[id];
}
