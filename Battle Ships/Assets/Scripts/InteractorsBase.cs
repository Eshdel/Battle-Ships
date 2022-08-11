using System;
using System.Collections.Generic;
using BattleShips.Scenes.Scripts;

namespace BattleShips.Architecture {
    public class InteractorsBase {

        private Dictionary<Type,Interactor> interactorsMap;
        private SceneConfig sceneConfig;

        public InteractorsBase(SceneConfig config) {
            sceneConfig = config;
        }

        public void CreateAllInteractos() {
            interactorsMap = sceneConfig.CreateAllInteractors();
        }

        public void SendOnCreateToAllInteractors() {
            var allInteractors = interactorsMap.Values;
            foreach (var interactor in allInteractors) {
                interactor.OnCreate();
            }
        }

        public void IntializeAllInteractors () {
            var allInteractors = interactorsMap.Values;
            foreach (var interactor in allInteractors) {
                interactor.Initialize();
            }
        }

        public void SendOnStartToAllInteractors () {
            var allInteractors = interactorsMap.Values;
            foreach (var interactor in allInteractors) {
                interactor.OnStart();
            }
        }

        public T GetInteractor<T>() where T:Interactor {
            var type = typeof(T);
            return (T) interactorsMap[type];
        }
    }
}
