using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BattleShips.Architecture {
    public class ShipsInteractor : Interactor
    {
        private ShipsRepository repository;
    
        public ShipsInteractor() {
        }

        public void AddShip(object sender, string ship) {
            repository.ships += ship;
            this.repository.Save();
        }
    }
}
