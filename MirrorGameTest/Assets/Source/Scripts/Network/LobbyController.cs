using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class LobbyController : MonoBehaviour
{
    public static LobbyController Instance;
    
    public GameObject LocalPlayerGameObject;
    
    public ulong CurrentLobbyID;
    public PlayerNetworkController localPlayerController;
    
    [SerializeField] private TextMeshProUGUI lobbyNameText;
    
    
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

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void UpdateLobbyName()
    {
        CurrentLobbyID = NetworkManager.GetComponent<SteamLobby>().CurrentLobbyID;

        if (lobbyNameText != null)
        {
            lobbyNameText.text = SteamMatchmaking.GetLobbyData(new CSteamID(CurrentLobbyID), "name");
        }
    }

    public void FindLocalPlayer()
    {
        LocalPlayerGameObject = GameObject.Find("LocalGamePlayer");
        localPlayerController = LocalPlayerGameObject.GetComponent<PlayerNetworkController>();
    }
}
