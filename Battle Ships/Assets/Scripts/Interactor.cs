namespace BattleShips.Architecture {
    public class Interactor {

        public virtual void OnCreate() {} // When all interactors crated

        public virtual void Initialize() {} // When interactors do OnCreate()

        public virtual void OnStart() {}

    }
}
