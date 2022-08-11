using System;

namespace BattleShips.Architecture {
    public static class Ships
    {

        public static event Action OnShipsInitializeEvent;

        public static string ships{ 
            get{
                CheckClass();
                return shipsInteractor.ships;
            }
        }

        private static ShipsInteractor shipsInteractor;

        public static bool isInitialize{get; private set;} = false;

        public static void Initialize(ShipsInteractor interactor) {
            shipsInteractor = interactor;
            isInitialize = true;
            OnShipsInitializeEvent?.Invoke();
        }

        public static void AddShip(object sender, string ship) {
            CheckClass();
            shipsInteractor.AddShip(sender,ship);
        }

        private static void CheckClass() {
            if(!isInitialize)
                throw new System.Exception("Ships  is not initalize yet");
        }
    }
}
