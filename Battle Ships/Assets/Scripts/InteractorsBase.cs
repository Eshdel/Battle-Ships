using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleShips.Architecture {
    public class InteractorsBase {

        private Dictionary<Type,Interactor> interactorsMap;

        public InteractorsBase() {
            this.interactorsMap = new Dictionary<Type, Interactor>();
        }

        public void CreateAllInteractos() {
            CreateInteractor<ShipsInteractor>();
        }

        private void CreateInteractor<T>() where T : Interactor, new () {
            var interactor = new T();
            var type  = typeof(T);
            interactorsMap[type] = interactor;
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

        private T GetInteractor<T>() where T:Interactor {
            var type = typeof(T);
            return (T) interactorsMap[type];
        }
    }
}
