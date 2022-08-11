using System;
using System.Collections.Generic;
using BattleShips.Architecture;

namespace BattleShips.Scenes.Scripts {
    public class SceneConfigExample : SceneConfig {
       
        public const string SCENE_NAME = "SampleScene";
        public override string sceneName => SCENE_NAME;

        public override Dictionary<Type, Interactor> CreateAllInteractors()
        {
            var interactorsMap = new Dictionary<Type,Interactor>();
            CreateInteractor<ShipsInteractor>(interactorsMap);
            return interactorsMap;
        }

        public override Dictionary<Type, Repository> CreateAllRepositories()
        {
            var repositoriesMap = new Dictionary<Type,Repository>();
            CreateRepository<ShipsRepository>(repositoriesMap);
            return repositoriesMap;
        }
    }
}