using Mirror;
using UnityEngine;

[RequireComponent(typeof(PlayerInventory))]
public class PlayerHand : NetworkBehaviour
{
    private const int HandId = 0;

    [SerializeField] private KeyCode _slotKey1 = KeyCode.Alpha1;
    [SerializeField] private KeyCode _slotKey2 = KeyCode.Alpha2;
    [SerializeField] private KeyCode _slotKey3 = KeyCode.Alpha3;
    [SerializeField] private KeyCode _slotKey4 = KeyCode.Alpha4;

    [SerializeField] private Transform _handSocket;

    private PlayerInventory _inventory;

    [SyncVar(hook = nameof(OnSelectedSlotHook))]
    private int _selectedSlot;

    private void Awake()
    {
        _selectedSlot = HandId;
        _inventory = GetComponent<PlayerInventory>();
    }
     
    private void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetKeyDown(_slotKey1)) CmdSelectSlot(HandId);
        if (Input.GetKeyDown(_slotKey2)) CmdSelectSlot(1);
        if (Input.GetKeyDown(_slotKey3)) CmdSelectSlot(2);
        if (Input.GetKeyDown(_slotKey4)) CmdSelectSlot(3);
    }

    public int GetSelectedSlot() => _selectedSlot;

    [Command]
    public void CmdSelectSlot(int newSlot)
    {
        if (newSlot < 0 || newSlot > 3) return;

        if (_selectedSlot == HandId && newSlot != HandId)
        {
            if (_inventory.GetItemById(HandId) != null)
            {
                _inventory.DropItemById(HandId);
            }
        }

        _selectedSlot = newSlot;
    }

    private void OnSelectedSlotHook(int oldValue, int newValue)
    {
        // here we need to rig new model to the player's hand
    }

}
