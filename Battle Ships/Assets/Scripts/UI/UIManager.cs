using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

public class UIManager : MonoBehaviour
{
    private static UIManager _singleton;
    
    public Navigation mainMenuNavigation;
    
    public Navigation gameMenuNavigation;
    
    public static UIManager Singleton {
        get => _singleton;

        private set 
        {
            if(_singleton == null) {
                _singleton = value;
            }

            else {
                Debug.Log($"{nameof(UIManager)} instance already exist, destroying duplicate");
                Destroy(value);
            }
        }
    }

    private void Awake() {
        Singleton = this;
    }
}
