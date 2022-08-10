using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleShips.Architecture {
    public class Tester : MonoBehaviour
    {
        // Start is called before the first frame update
        private ShipsRepository repository;
        private ShipsInteractor interactor;
        
        void Start()
        {
            repository = new ShipsRepository();
            repository.Initialize();

            interactor = new ShipsInteractor();
            interactor.Initialize();
            
        }

        void Update() {
            if(Input.GetKeyDown(KeyCode.A)){
                interactor.AddShip(this,"Onichan");
            }

            if(Input.GetKeyDown(KeyCode.D)){
                interactor.AddShip(this,"Momy");
            }

            Debug.Log(repository.ships);
        }
    }
}   
