using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UNET;
using Unity.VisualScripting;
using UnityEngine.UI;

public class Logger : NetworkBehaviour
{
    [SerializeField] private TMP_Text textMeshPro;
 
    void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += Deds;
    }

    void Deds(ulong c)
    {

        
        if (NetworkManager.Singleton.ConnectedClients[c].PlayerObject.IsLocalPlayer && NetworkManager.Singleton.IsHost) {
            textMeshPro.text += "Connect new user Host:";
        }
        // Dont work by client
        if (!NetworkManager.Singleton.IsHost)
            textMeshPro.text += "Connect new user Client:" + c;
        
    }

    public override void OnDestroy()
    {
        NetworkManager.Singleton.OnClientConnectedCallback -= Deds;
    }
}
