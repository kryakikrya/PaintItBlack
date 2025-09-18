using UnityEngine;
using System.Collections.Generic;
using Mirror;
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private PlayerHand _hand;
    private NetworkIdentity _handItem;

    private List<Item> _items = new List<Item>(new Item[4]);

    public void PickUp(Item item)
    {
        int id = _hand.GetSelectedSlot();

        print(id);

        if (_items[id] != null)
        {
            DropItemById(id);
        }

        _items[id] = item;

        _hand.CmdSelectSlot(id);

        foreach (Item myItem in _items)
        {
            print(myItem);
        }
    }

    public void DropItemById(int id)
    {
        // to do: throw away an item
    }

    public Item GetItemById(int id) => _items[id];
}
