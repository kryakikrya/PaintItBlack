using Mirror;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Bootstrapper : NetworkBehaviour
{
    [SerializeField] private string _sceneAddress;

    private readonly HashSet<int> _readyConnections = new();

    private void Start()
    {
        if (isServer)
        {
            RpcLoadScene(_sceneAddress);
        }
    }

    [ClientRpc]
    private void RpcLoadScene(string address)
    {
        Addressables.LoadSceneAsync(address, LoadSceneMode.Single);
    }

}