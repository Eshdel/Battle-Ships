using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyFragment : Fragment
{
    // Start is called before the first frame update

    [SerializeField] private Button disconnectButton;

    [SerializeField] private Button playButton;
    
    public override void SetActionOnView() { }

    public override void Open()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback += Close;
        UIManager.Singleton.mainMenuNavigation.HideBackButton();
        base.Open();
    }

    public override void Close()
    {
        UIManager.Singleton.mainMenuNavigation.ShowBackButton();
        NetworkManager.Singleton.OnClientDisconnectCallback -= Close;
        base.Close();
    }

    private void Start()
    {
        disconnectButton.onClick.AddListener(ShutDowmn);
        playButton.onClick.AddListener(StartGame);
        
    }

    private void OnDestroy()
    {
        disconnectButton.onClick.RemoveListener(ShutDowmn);
        playButton.onClick.RemoveListener(StartGame);
    }

    private void ShutDowmn()
    {
        if (NetworkManager.Singleton.IsServer)
        {

            for (int i = NetworkManager.Singleton.ConnectedClients.Count -1; i > 0; i--)
            {
                NetworkManager.Singleton.DisconnectClient((ulong)i);
            }

        }
        
        NetworkManager.Singleton.Shutdown();
        UIManager.Singleton.mainMenuNavigation.GoBack();
    }

    private void Close(ulong id)
    {
        if (!NetworkManager.Singleton.IsServer)
            UIManager.Singleton.mainMenuNavigation.GoBack();   
        
    }

    private void StartGame()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("Game",LoadSceneMode.Single);
    }
    
}
