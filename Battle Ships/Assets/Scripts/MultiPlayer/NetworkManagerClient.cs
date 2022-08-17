using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;
using System;


public class NetworkManagerClient : MonoBehaviour
{
    private static NetworkManagerClient _singleton;

    [SerializeField] private string ip;
    [SerializeField] private ushort port;
    
    
    public Client Client {get;private set;}
    
    public static NetworkManagerClient Singleton {
        get => _singleton;

        private set 
        { 
            if(_singleton == null) {
                _singleton = value;
            }

            else {
                Debug.Log($"{nameof(NetworkManagerClient)} instance already exist, destroying duplicate");
                Destroy(value);
            }
        }
    }

    private void Awake() {
        Singleton = this;    
    }

    void Start() {
        RiptideLogger.Initialize(Debug.Log,Debug.Log,Debug.LogWarning,Debug.LogError,false);
        
        Client = new Client();
        Client.Connected += DidConnect;
        Client.ConnectionFailed += FailedConnect;
        Client.ClientDisconnected += DidDisconnect;
    }

    private void FixedUpdate() {
        Client.Tick();
    }

    public void Connect() {
        Client.Connect($"{ip}:{port}");
    }
    
    private void OnApplicationQuit() {
        Client.Disconnect();
    }

    private void DidConnect(object sender,EventArgs e) {
        UIManager.Singleton.SendName();
    }

    private void FailedConnect(object sender,EventArgs e) {}

    private void DidDisconnect(object sender,EventArgs e) {}
    
}
