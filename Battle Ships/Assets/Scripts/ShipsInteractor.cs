using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BattleShips.Architecture {
    public class ShipsInteractor : Interactor
    {
        private ShipsRepository repository;
        
        public ShipsInteractor(ShipsRepository repository) {
            this.repository = repository;
        }

        public void AddShip(object sender, string ship) {
            repository.ships += ship;
            this.repository.Save();
        }
    }
}
