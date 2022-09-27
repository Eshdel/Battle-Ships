using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TestMessages : NetworkBehaviour
{
    public NetworkVariable<int> countClicks = new NetworkVariable<int>(0);

    public delegate void updateClick(int countP,int countNew);

    public event updateClick clickHandler;

    public override void OnNetworkSpawn() {
        countClicks.OnValueChanged += OnValueChanged;
    }

    public override void OnNetworkDespawn() {
        countClicks.OnValueChanged -= OnValueChanged;
    }

    public void OnValueChanged(int prev, int cur) {
        clickHandler.Invoke(prev,cur);
    }
    
    [ServerRpc(RequireOwnership = false)]
    public  void IncreaseClickServerRpc()
    {
        // this will cause a replication over the network
        // and ultimately invoke `OnValueChanged` on receivers
        countClicks.Value++;
        Debug.Log(countClicks.Value);
        NetworkManager.Singleton.SceneManager.LoadScene("Game",LoadSceneMode.Single);
    }
}
