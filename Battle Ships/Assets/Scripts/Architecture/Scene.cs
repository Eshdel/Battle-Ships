using System.Collections;
using BattleShips.Architecture;
using UnityEngine;

namespace BattleShips.Scenes.Scripts {
    public class Scene {

        private InteractorsBase interactorsBase;
        private RepositoryBase repositoryBase;
        private SceneConfig sceneConfig;

        public Scene(SceneConfig config) {
            sceneConfig = config;
            interactorsBase = new InteractorsBase(config);
            repositoryBase = new RepositoryBase(config);
        }

        private IEnumerator Initialize() {
            interactorsBase.CreateAllInteractos();
            repositoryBase.CreateAllRepositories();
            yield return null;

            interactorsBase.SendOnCreateToAllInteractors();
            repositoryBase.SendOnCreateToAllRepositories();
            yield return null;

            interactorsBase.IntializeAllInteractors();
            repositoryBase.IntializeAllRepositories();
            yield return null;            
            
            interactorsBase.SendOnStartToAllInteractors();
            repositoryBase.SendOnStartToAllRepositories();
        }

        public T GetRepository<T>() where T: Repository {
            return repositoryBase.GetRepository<T>();
        }

        public T GetInteractor<T>() where T:Interactor {
            return interactorsBase.GetInteractor<T>();
        }

        public Coroutine InitializeAsync() {
            return Coroutines.StartRoutine(Initialize());
        }
    }
}
