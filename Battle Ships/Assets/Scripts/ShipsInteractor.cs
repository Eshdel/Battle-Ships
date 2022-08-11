
namespace BattleShips.Architecture {
    public class ShipsInteractor : Interactor
    {
        private ShipsRepository repository;

        public string ships => this.repository.ships;

        public void AddShip(object sender, string ship) {
            repository.ships += ship;
            this.repository.Save();
        }

        public override void Initialize()
        {
            base.Initialize();
            Ships.Initialize(this);
        }

        public override void OnCreate()
        {
            base.OnCreate();
            repository = Game.GetRepository<ShipsRepository>();
        }
    }
}
