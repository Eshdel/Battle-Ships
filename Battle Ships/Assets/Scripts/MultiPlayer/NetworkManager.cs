using UnityEngine;
using RiptideNetworking;
using RiptideNetworking.Utils;

enum ClientToServerId:ushort{
    name = 1,
}

public class NetworkManager : MonoBehaviour {
   
    private static NetworkManager _singleton;

    public static NetworkManager Singleton {
        get => _singleton;

        private set 
        {
            if(_singleton == null) {
                _singleton = value;
            }

            else {
                Debug.Log($"{nameof(NetworkManager)} instance already exist, destroying duplicate");
                Destroy(value);
            }
        }
    }
    
    [SerializeField] private ushort port;
    [SerializeField] private  ushort maxClientCount;
    
    public Server Server {get;private set;}
    
    private void Awake() {
        Singleton = this;
    }

    private void FixedUpdate() {
        if(Server != null) 
            Server.Tick();
    }

    private void OnApplicationQuit() {
        Server.Stop();
    }

    public void CreateServer() {
        RiptideLogger.Initialize(Debug.Log,Debug.Log,Debug.LogWarning,Debug.LogError,false);
        Server = new Server();
        Server.Start(port,maxClientCount);
        Server.ClientDisconnected += PlayerLeft;
    }

    private void PlayerLeft(object sender, ClientDisconnectedEventArgs e){ 
        Destroy(Host.list[e.Id].gameObject);
    }


}
