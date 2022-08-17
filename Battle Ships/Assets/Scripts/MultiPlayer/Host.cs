using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiptideNetworking;

public class Host : Player
{
    public static Dictionary<ushort,Player> list = new Dictionary<ushort, Player>();

    public static void Spawn(ushort id, string username) {
        Player player = Instantiate(GameLogic.Singleton.PlayerPrefab,new Vector3(0,0,0),Quaternion.identity).GetComponent<Player>();
        player.name = $"Player {id} ({(string.IsNullOrEmpty(username) ? "Guest" : username)})";
        player.Id = id;
        player.Username = string.IsNullOrEmpty(username) ? "Guest" : username;

        list.Add(id,player);
    }

    [MessageHandler((ushort)ClientToServerId.name)]
    private static void Name(ushort fromClientId,Message message) {
        Spawn(fromClientId,message.GetString());
    }
    
    private void OnDestroy() {
        list.Remove(Id);
    }
}
