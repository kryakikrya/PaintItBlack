using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using Steamworks;

public class CustomNetworkManager : NetworkManager
{
    public List<PlayerNetworkController> Players { get; private set; } = new List<PlayerNetworkController>();
    
    [SerializeField] private PlayerNetworkController _playerNetworkPrefab;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            PlayerNetworkController gamePlayerIstance = Instantiate(_playerNetworkPrefab);
            
            gamePlayerIstance.ConnectionID = conn.connectionId;
            gamePlayerIstance.PlayerID = Players.Count + 1;
            gamePlayerIstance.PlayerSteamID = (ulong)SteamMatchmaking.GetLobbyMemberByIndex
                ((CSteamID)SteamLobby.Instance.CurrentLobbyID, Players.Count);
            
            NetworkServer.AddPlayerForConnection(conn, gamePlayerIstance.gameObject);
        }
    }
}
