using System;
using System.Collections.Generic;
using BattleShips.Scenes.Scripts;

namespace BattleShips.Architecture {
    public class RepositoryBase {

        private Dictionary<Type,Repository> repositoriesMap;
        private SceneConfig sceneConfig;

        public RepositoryBase(SceneConfig config) {
           sceneConfig = config;
        }

        public void CreateAllRepositories() {
            repositoriesMap = sceneConfig.CreateAllRepositories();
        }

        public void SendOnCreateToAllRepositories() {
            var allInteractors = repositoriesMap.Values;
            foreach (var repository in allInteractors) {
                repository.OnCreate();
            }
        }

        public void IntializeAllRepositories () {
            var allInteractors = repositoriesMap.Values;
            foreach (var repository in allInteractors) {
                repository.Initialize();
            }
        }

        public void SendOnStartToAllRepositories () {
            var allInteractors = repositoriesMap.Values;
            foreach (var repository in allInteractors) {
                repository.OnStart();
            }
        }

        public T GetRepository<T>() where T:Repository {
            var type = typeof(T);
            return (T) repositoriesMap[type];
        }
    }
}

