using System;
using System.Collections;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class MultiplayerFragment : Fragment
{
    private View[] typeConnection;
    private int curIndex = 0;

    [SerializeField] private Fragment LobbyFragement;
    
    [SerializeField] Lobby lobby;

    [SerializeField] private LobbyUI lobbyUI;

    [SerializeField] private Button playButton;

    [SerializeField] private TMP_InputField NameInputField;
        
    [SerializeField] private View Host;
    
    [SerializeField] private View Client;
    
    private bool isHost;


    private void OnEnable()
    {
        isHost = true;
        ChangeTypeConnectionView();
    }

    public override void SetActionOnView()
    {
        _views[6].Action = () =>
        {
            isHost = !isHost;
            ChangeTypeConnectionView();
        };
        
        _views[7].Action = () =>
        {
            isHost = !isHost;

            ChangeTypeConnectionView();
        };
       
    }

    public override void OnStart()
    {
       
        playButton.onClick.AddListener(Play);
    }

    private void ChangeTypeConnectionView()
    {
        if (isHost)
        {
            Host.Show();
            Client.Hide();
            
        }   
        else
        {
            Client.Show();
            Host.Hide();
        }

    }
    
    private void OpenLobbyFragement()
    {
        UIManager.Singleton.mainMenuNavigation.Add(LobbyFragement); 
    }

    private void Play()
    {
        OpenLobbyFragement();
        
        if (isHost)
            StartHost();
        
        else
            ConnnectToHost();
        
        InputPlayerDataClass.PlayerNameFromInputLabel = NameInputField.text;
    }
    
    private void StartHost()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback = ApprovalCheck;
        NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes("Host");
        NetworkManager.Singleton.StartHost();
        lobby.AddHost();
    }

    private void ConnnectToHost()
    {
        NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes("Guest");
        NetworkManager.Singleton.StartClient();
    }
    
    void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        var clientId = request.ClientNetworkId;

        var connectionData = request.Payload;
        
        // Your approval logic determines the following values
        response.Approved = true;
        response.CreatePlayerObject = true;

        // The prefab hash value of the NetworkPrefab, if null the default NetworkManager player prefab is used
        response.PlayerPrefabHash = null;

        // Position to spawn the player object (if null it uses default of Vector3.zero)
        response.Position = Vector3.zero;

        // Rotation to spawn the player object (if null it uses the default of Quaternion.identity)
        response.Rotation = Quaternion.identity;

        // If additional approval steps are needed, set this to true until the additional steps are complete
        // once it transitions from true to false the connection approval response will be processed.
        response.Pending = false;
    }
}
