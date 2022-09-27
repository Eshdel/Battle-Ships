using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class PlayerSpawner : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (IsServer)
        {
            foreach (var pair in NetworkManager.Singleton.ConnectedClients  )
            {

                Player player = pair.Value.PlayerObject.GetComponent<Player>(); 
                
                foreach (var prefab in player.Fleet.ShipsPrefab)
                    prefab.UpdateCount();
                
                Debug.Log("TryLoad");
                player.Fleet.battleField.InitalizeAreas();
                
                Debug.Log("InitArea");
                player.Fleet.InitialShips();
                Debug.Log("InitSh");
                if (pair.Key == 0)
                {
                    player.Fleet.battleField.Offset = new Vector2(4.5f,-4.5f);
                }
                else
                {
                    player.Fleet.battleField.Offset = new Vector2(-8.5f,-4.5f);
                }
                
                player.Fleet.battleField.CreateField(player);
                Debug.Log("CreateField");
                // player.Fleet.PlaceFleetOnBattleField();
                // Debug.Log("PlaceShips");

                player.Controller.tryAttackPos.OnValueChanged += PlayerAttackTryAttackPos;
            }
            
            GetFirstPlayerTurn();
        }
    }

    private void PlayerAttackTryAttackPos(Vector3 previousvalue, Vector3 newvalue)
    {
        if (NetworkManager.Singleton.IsServer)
        {
            var player = FindObjectOfType<Enemy>().GetComponent<Player>();
            
            if (player != null);
            {

                Debug.Log(player.PlayerName+ " Was attack on" + newvalue);
                //player.Fleet.battleField.AttackArea((int)newvalue.y,(int)newvalue.x);
            }
        }
    }


    
    
    public void GetFirstPlayerTurn()
    {
        NetworkManager.Singleton.ConnectedClients.First().Value.PlayerObject.GetComponent<Player>().Controller.IsYourTurn.Value = true;
    }
    
}
