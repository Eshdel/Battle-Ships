namespace BattleShips.Architecture {
    public abstract class Repository {
   
        public abstract void Initialize();

        public virtual void OnCreate() {}

        public virtual void OnStart() {}

        public abstract void Save();
    }
}
