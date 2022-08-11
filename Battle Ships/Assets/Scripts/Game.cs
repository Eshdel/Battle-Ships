using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using BattleShips.Architecture;
using BattleShips.Scenes.Scripts;

public static class Game {

    public static event Action OnGameInitilizeEvent;

    public static SceneManagerBase sceneManager {get;private set;}

    public static void Run() {
        sceneManager = new SceneManagerExapmle();
        Coroutines.StartRoutine(InitilizeGame());
    }

    private static IEnumerator InitilizeGame(){
        sceneManager.InitScenesMap();
        yield return sceneManager.LoadCurrentSceneAsync();
        OnGameInitilizeEvent?.Invoke();
    }
 
    public static T GetRepository<T>() where T: Repository {
        return sceneManager.GetRepository<T>();
    }

    public static T GetInteractor<T>() where T:Interactor {
        return  sceneManager.GetInteractor<T>();
    }
}
