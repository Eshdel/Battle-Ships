using UnityEngine;
 
namespace BattleShips.Architecture {
 
    public class ShipsRepository : Repository {
        private const string KEY = "BANK_KEY";
        public string ships {get;set;}
        
        public override void Initialize() {
            this.ships = PlayerPrefs.GetString(KEY,"");
        }

        public override void Save() {
            PlayerPrefs.SetString(KEY,ships);
        }
    }
}
