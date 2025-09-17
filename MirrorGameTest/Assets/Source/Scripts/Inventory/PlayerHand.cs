using Mirror;
using UnityEngine;

[RequireComponent(typeof(PlayerInventory))]
public class PlayerHand : NetworkBehaviour
{
    private const int HandId = 0;

    [SerializeField] private readonly KeyCode _slotKey1 = KeyCode.Alpha1;
    [SerializeField] private readonly KeyCode _slotKey2 = KeyCode.Alpha2;
    [SerializeField] private readonly KeyCode _slotKey3 = KeyCode.Alpha3;
    [SerializeField] private readonly KeyCode _slotKey4 = KeyCode.Alpha4;

    [SerializeField] private Transform _handSocket;

    private PlayerInventory _inventory;
    [SyncVar] private int _selectedSlot = 0; // to do: UniRx UI changes

    private void Awake()
    {
        _inventory = GetComponent<PlayerInventory>();
    }
     
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) _selectedSlot = HandId;
        if (Input.GetKeyDown(KeyCode.Alpha2)) _selectedSlot = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) _selectedSlot = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4)) _selectedSlot = 3;
    }

    [Command]
    private void OnSelectedSlotChanged()
    {
        if (_selectedSlot != HandId && _inventory.GetItemById(HandId) != null)
        {
            _inventory.DropItemById(HandId);
        }

        GameObject item = _inventory.GetItemById(_selectedSlot).gameObject;
    }
}
