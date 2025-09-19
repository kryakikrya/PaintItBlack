using Mirror;
using Steamworks;
using UnityEngine;

public class PlayerNetworkController : NetworkBehaviour
{
    [SerializeField] private GameObject _camera;

    [SyncVar(hook = nameof(PlayerNameUpdate))] public string PlayerName;
    
    [SyncVar] public int ConnectionID;
    [SyncVar] public int PlayerID;
    [SyncVar] public ulong PlayerSteamID;

    private CustomNetworkManager networkManager;
    
    private CustomNetworkManager NetworkManager
    {
        get
        {
            if (networkManager != null)
            {
                return networkManager;
            }
            return networkManager = CustomNetworkManager.singleton as CustomNetworkManager;
        }
    }

    private void Start()
    {
        if (!isLocalPlayer) return;
        DontDestroyOnLoad(this.gameObject);
        _camera.SetActive(true);
    }

    public override void OnStartAuthority()
    {
        CmdSetPlayerName(SteamFriends.GetPersonaName().ToString());
        gameObject.name = "LocalGamePlayer";
        LobbyController.Instance.FindLocalPlayer();
        LobbyController.Instance.UpdateLobbyName();
    }

    public override void OnStartClient()
    {
        NetworkManager.Players.Add(this);
        LobbyController.Instance.UpdateLobbyName();
    }

    public override void OnStopClient()
    {
        NetworkManager.Players.Remove(this);
    }

    [Command]
    private void CmdSetPlayerName(string name)
    {
        this.PlayerNameUpdate(this.PlayerName, name);
    }

    public void PlayerNameUpdate(string oldPlayerName, string newPlayerName)
    {
        if (isServer)
        {
            this.PlayerName = newPlayerName;
        }
    }
}
