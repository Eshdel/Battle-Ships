using BattleShips.Architecture;
using BattleShips.Scenes.Scripts;
using UnityEngine;

public class Tester : MonoBehaviour {
    private void Start() {
            Game.Run();
    }

        void Update() {

            if(!Ships.isInitialize)
                return;

            if(Input.GetKeyDown(KeyCode.A)){
                Ships.AddShip(this,"Onichan");
                Debug.Log(Ships.ships);
            }

            if(Input.GetKeyDown(KeyCode.D)){
                Ships.AddShip(this,"Momy");
                Debug.Log(Ships.ships);
            }            
        }
}
 
