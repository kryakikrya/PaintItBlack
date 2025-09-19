using Mirror;
using UnityEngine;

public class LobbyDoor : InteractableObject
{
    public override void OnServerInteract(GameObject player)
    {
        if (NetworkManager.singleton is CustomNetworkManager network)
        {
            network.StartMatch();
        }
    }
}
