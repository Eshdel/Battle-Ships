using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class LobbyUI : NetworkBehaviour
{

    [SerializeField] private Lobby lobby;

    [SerializeField] private HolderFragment[] Holders;

    private void OnEnable()
    {
        foreach (var holder in Holders)
        {
            holder.gameObject.SetActive(false);
        }
    }
    
    public override void OnNetworkSpawn()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback += HideHolder;
        lobby.Players.OnListChanged += OnPlayersChanged;
        lobby.ChangePlayerNameEvent += ChangeName;
    }

    public override void OnNetworkDespawn()
    {
        lobby.Players.OnListChanged -= OnPlayersChanged;
        lobby.ChangePlayerNameEvent -= ChangeName;
        NetworkManager.Singleton.OnClientDisconnectCallback -= HideHolder;
    }

    public void OnPlayersChanged(NetworkListEvent<PlayerInLobby> changeevent)
    {
        if (changeevent.Value.Id == 0)
        {
            Holders[0].gameObject.SetActive(true);
            Holders[0].nameLabel.text = changeevent.Value.Name.Value;    
        }
        else
        {
            Holders[1].gameObject.SetActive(true);
            Holders[1].nameLabel.text = changeevent.Value.Name.Value;    
        }
        
    }

    private void HideHolder(ulong id)
    {
        if (id == 0)
            Holders[0].gameObject.SetActive(false);
        
        else
            Holders[1].gameObject.SetActive(false);
        
      
    }

    public void ChangeName(FixedString32Bytes old ,FixedString32Bytes cur)
    {
        if (NetworkManager.IsServer)
        {

            foreach (var VARIABLE in NetworkManager.Singleton.ConnectedClients)
            {
                Player player = NetworkManager.ConnectedClients[VARIABLE.Key].PlayerObject.GetComponent<Player>();
                if (VARIABLE.Key == 0)
                {
                    lobby.Players[0] = new PlayerInLobby(player.PlayerName.Value.Value,0);    
                }
                else
                {
                    lobby.Players[1] = new PlayerInLobby(player.PlayerName.Value.Value,1);
                }
                
            }
        }
    }
    
}
