using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Coroutines : MonoBehaviour {
    private static Coroutines instance {
        get {
            if(_instance == null) {
                var coroutineManager = new GameObject("COROUTINE MANAGER");
                _instance = coroutineManager.AddComponent<Coroutines>();
                DontDestroyOnLoad(coroutineManager);
            }
            return _instance;
        } 
    }

    private static Coroutines _instance;
    
    public static Coroutine StartRoutine(IEnumerator enumerator) {
        return instance.StartCoroutine(enumerator);
    }

    public static void StopRoutine(Coroutine coroutine) {
        if(coroutine != null)
            instance.StopCoroutine(coroutine);
    }
}
