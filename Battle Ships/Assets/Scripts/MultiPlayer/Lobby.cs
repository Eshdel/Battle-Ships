using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using Unity.Netcode.Transports.UNET;
using UnityEngine;

public class Lobby : NetworkBehaviour
{
    public UNetTransport uNetTransport;

    public NetworkList<PlayerInLobby> Players;

    public delegate void ChangePlayerNameHandler(FixedString32Bytes old ,FixedString32Bytes cur);

    public event ChangePlayerNameHandler ChangePlayerNameEvent;
    
    private void Awake()
    {
        Players = new NetworkList<PlayerInLobby>();
    }

    public override void OnNetworkSpawn()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += AddNewPlayer;
        NetworkManager.Singleton.OnClientDisconnectCallback += RemovePlayer;
    }

    public override void OnNetworkDespawn()
    {

        NetworkManager.Singleton.OnClientConnectedCallback -= AddNewPlayer;
        NetworkManager.Singleton.OnClientDisconnectCallback -= RemovePlayer;
        
    }
    
    public void AddNewPlayer(ulong id)
    {
        if (NetworkManager.Singleton.IsServer)
        {
            Player newPlayer = NetworkManager.Singleton.ConnectedClients[id].PlayerObject.GetComponent<Player>();
            PlayerInLobby playerInLobby = new PlayerInLobby(newPlayer.PlayerName.Value, 1);
            Players.Add(playerInLobby);

            newPlayer.PlayerName.OnValueChanged += UpdatePlayerName;
        }
    }

    private void RemovePlayer(ulong id)
    {
        if (NetworkManager.Singleton.IsServer)
        {
            Player newPlayer = NetworkManager.Singleton.ConnectedClients[id].PlayerObject.GetComponent<Player>();
            newPlayer.PlayerName.OnValueChanged -= UpdatePlayerName;

            if (id == 0)
                Players.Remove(Players[0]);    
            else
                Players.Remove(Players[1]);
        }   
    }
    
    public void AddHost()
    {
        if (NetworkManager.Singleton.IsServer) {  
            Debug.Log("Add Host");
            var Host = NetworkManager.Singleton.ConnectedClients[0].PlayerObject.GetComponent<Player>();
            var playerInLobby = new PlayerInLobby(Host.PlayerName.Value.Value,0);
            Host.PlayerName.OnValueChanged += UpdatePlayerName;
            Players.Add(playerInLobby);
        }
    }
    
    public void UpdatePlayerName(FixedString32Bytes old ,FixedString32Bytes cur)
    {
        ChangePlayerNameEvent.Invoke(old,cur);
    }
}
