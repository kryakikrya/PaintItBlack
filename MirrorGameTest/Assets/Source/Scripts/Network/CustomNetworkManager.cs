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
        if (SceneManager.GetActiveScene().name == "Bar")
        {
            PlayerNetworkController gamePlayerIstance = Instantiate(_playerNetworkPrefab);
            
            gamePlayerIstance.ConnectionID = conn.connectionId;
            gamePlayerIstance.PlayerID = Players.Count + 1;
            gamePlayerIstance.PlayerSteamID = (ulong)SteamMatchmaking.GetLobbyMemberByIndex
                ((CSteamID)SteamLobby.Instance.CurrentLobbyID, Players.Count);
            
            NetworkServer.AddPlayerForConnection(conn, gamePlayerIstance.gameObject);
        }
    }
    public override void OnServerSceneChanged(string sceneName)
    {
        base.OnServerSceneChanged(sceneName);

        var startPositions = FindObjectsOfType<NetworkStartPosition>();

        if (startPositions.Length == 0)
        {
            Debug.LogWarning("No NetworkStartPosition found on scene.");
            return;
        }

        int index = 0;

        foreach (var connections in NetworkServer.connections.Values)
        {
            if (connections?.identity == null) continue;

            var player = connections.identity.gameObject;
            var target = startPositions[index % startPositions.Length].transform;

            player.transform.SetPositionAndRotation(target.position, target.rotation);
            index++;
        }
    }

    [Server]

    public void StartMatch()
    {
        ServerChangeScene("Location");
    }
}
