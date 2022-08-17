using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private static GameLogic _singleton;
    
    public static GameLogic Singleton {
        get => _singleton;

        private set 
        {
            if(_singleton == null) {
                _singleton = value;
            }

            else {
                Debug.Log($"{nameof(GameLogic)} instance already exist, destroying duplicate");
                Destroy(value);
            }
        }
    }

    private void Awake() {
        Singleton = this;
    }

    [SerializeField] private Player playerPrefab;

    public  Player PlayerPrefab {
        get => playerPrefab;
    }
}
