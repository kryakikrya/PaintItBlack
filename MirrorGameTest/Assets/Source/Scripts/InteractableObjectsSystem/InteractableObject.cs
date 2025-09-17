using Mirror;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : NetworkBehaviour
{

    [SerializeField] private List<Tag> _tags = new();
    public IReadOnlyList<Tag> Tags => _tags;

    [SyncVar] protected bool _canInteract;

    [Server] public bool GetCanInteract() => _canInteract;
    [Server] public virtual void OnServerInteract(GameObject player) { } // to do: change to the player's script

    [Command(requiresAuthority = false)]
    public void CommandInteract(NetworkIdentity player)
    {
        if (_canInteract)
        {
            if (!isServer) return;
            if (player.gameObject != null)
            {
                OnServerInteract(player.gameObject);
            }
        }
    }
}
