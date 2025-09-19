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
        Transform start = GetStartPosition();
        PlayerNetworkController gamePlayerIstance = Instantiate(_playerNetworkPrefab, start.position, start.rotation);

        if (SceneManager.GetActiveScene().name == "Bar")
        {
            gamePlayerIstance.ConnectionID = conn.connectionId;
            gamePlayerIstance.PlayerID = Players.Count + 1;
            gamePlayerIstance.PlayerSteamID = (ulong)SteamMatchmaking.GetLobbyMemberByIndex
                ((CSteamID)SteamLobby.Instance.CurrentLobbyID, Players.Count);
            
            NetworkServer.AddPlayerForConnection(conn, gamePlayerIstance.gameObject);
        }
    }
    public override void OnServerSceneChanged(string sceneName)
    {
        if (sceneName == "Location") // твоя игровая сцена
        {
            var startPositions = FindObjectsOfType<NetworkStartPosition>();

            int i = 0;
            foreach (var conn in NetworkServer.connections.Values)
            {
                if (conn.identity != null)
                {
                    var player = conn.identity.gameObject;
                    var pos = startPositions[i % startPositions.Length];
                    player.transform.SetPositionAndRotation(pos.transform.position, pos.transform.rotation);
                    i++;
                }
            }
        }
    }


    [Server]
    public void StartMatch()
    {
        Transform start = GetStartPosition();
        ServerChangeScene("Location");
    }
}
