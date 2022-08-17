using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using BattleShips.Architecture;
namespace BattleShips.Scenes.Scripts { 
    public abstract class SceneManagerBase
    {
        public event Action<Scene> OnSceneLoadedEvent;

        private float MAX_PROGRESS_LOADING_NEW_SCENE = 0.9f;
        public Scene scene {get;private set;}
        public bool isLoading{get;private set;} = false;
        public Dictionary<string,SceneConfig> sceneConfigMap;

        public SceneManagerBase() {
            sceneConfigMap = new Dictionary<string,SceneConfig>();
        }

        public abstract void InitScenesMap();

        public Coroutine LoadNewSceneAsync(string sceneName) {
            if (isLoading) 
                throw new Exception("Scene is loading now");

            var config = sceneConfigMap[sceneName];
            return Coroutines.StartRoutine(LoadNewScene(config));
        }

        public Coroutine LoadCurrentSceneAsync() {
            if (isLoading) 
                throw new Exception("Scene is loading now");

            var sceneName = SceneManager.GetActiveScene().name;
            var config = sceneConfigMap[sceneName];
            return Coroutines.StartRoutine(LoadCurrentScene(config));
        }

        private IEnumerator LoadCurrentScene(SceneConfig config) {
            isLoading = true;
            
            yield return Coroutines.StartRoutine(IntializeScene(config));

            isLoading = false;
            OnSceneLoadedEvent?.Invoke(scene);
        }

        private IEnumerator LoadNewScene(SceneConfig config) {
            isLoading = true;
            
            yield return Coroutines.StartRoutine(LoadScene(config));
            yield return Coroutines.StartRoutine(IntializeScene(config));

            isLoading = false;
            OnSceneLoadedEvent?.Invoke(scene);
        }

        private IEnumerator LoadScene(SceneConfig sceneConfig) {
            var async = SceneManager.LoadSceneAsync(sceneConfig.sceneName);
            async.allowSceneActivation = false;

            while(async.progress < MAX_PROGRESS_LOADING_NEW_SCENE) {
                yield return null;
            }

            async.allowSceneActivation = true;
        }

        private IEnumerator IntializeScene(SceneConfig sceneConfig) {
            scene = new Scene(sceneConfig);
            yield return scene.InitializeAsync();
        }

        public T GetRepository<T>() where T: Repository {
            return scene.GetRepository<T>();
        }

        public T GetInteractor<T>() where T:Interactor {
            return scene.GetInteractor<T>();
        }
         
    }
}