using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Fleet : NetworkBehaviour
{

    public ShipPrefab[] ShipsPrefab;

    public NetworkList<Ship> Ships = new NetworkList<Ship>();

    public BattleField battleField;
    
    public void InitialShips()
    {
        foreach (var prefab in ShipsPrefab)
        {
            prefab.UpdateCount();
            for (int i = 0; i < prefab.Count; i++)
            {
                Ships.Add(new Ship(prefab.Lenght,prefab.Width,prefab.Lenght * prefab.Width,Vector2.zero));
            } 
          
        }
    }
    
    public void PlaceFleetOnBattleField() 
    {
        foreach (var ship in Ships)
        {
            int indRow = 0;
            int indColumn = 0;
            
            while (indRow < battleField.Size || indColumn < battleField.Size)
            {
                var row = indRow;//Random.Range(0, (int)battleField.Size - 1);
                var column = indColumn;//Random.Range(0, (int)battleField.Size - 1);
                Debug.Log($"Try place on {row},{column} Lenght ={ship.Lenght}");
                if (battleField.PlaceObject(row, column, (int)ship.Lenght, (int)ship.Width))
                {
                    Ship ship1 = ship;
                    ship1.Coordinates = new Vector2(row, column);

                    foreach (var shipPrefab in ShipsPrefab)
                    {
                        if (shipPrefab.Count > 0)
                        {
                            var obj = Instantiate(shipPrefab,new Vector3(column + (battleField.Offset.x + battleField.Offset.y),0f,-row),Quaternion.identity);
                            obj.GetComponent<NetworkObject>().Spawn();
                            var prefab = shipPrefab;
                            prefab.Ship = ship1;
                            Debug.Log($"Place ship in Array {row},{column}");
                            prefab.Count--;
                            break;
                        }
                    }
                    
                    break;
                }

                if (indColumn== battleField.Size -1 && indRow == battleField.Size -1)
                {
                    break;
                }
                
                if(indColumn < battleField.Size)
                    indColumn++;
                
                else
                {
                    indRow++;
                    indColumn = 0;
                }
               
            }
            
            for(int row = 0; row< battleField.Size;row++)
            {
                var str = "";
                
                for(int column = 0; column< battleField.Size;column++) {
                    str += Convert.ToInt16(battleField.fillAreasList[battleField.Index(row, column)])  + " ";
                }
                
                Debug.Log(str);
            }
        }
    }
}
