using UnityEngine;
using System.Collections.Generic;
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform _handSocket;

    private int _currentSlotId;

    private List<Item> _items = new List<Item>(4);

    public void TryPickUp(Item item)
    {
        if (_items[_currentSlotId] != null)
        {
            _items[_currentSlotId] = item;
        }
    }
}
