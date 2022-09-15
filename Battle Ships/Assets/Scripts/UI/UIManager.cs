using RiptideNetworking;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    
    private static UIManager _singleton;
    
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

    public Navigation mainMenuNavigation;
    public Navigation gameMenuNavigation;
    
    [Header("Connect")]
    [SerializeField] private GameObject connectUI;

    [SerializeField] private GameObject hostUI;

    [SerializeField] private TMP_InputField usernameField;

    public void ConnectClicked() {
        usernameField.interactable = false;
        hostUI.SetActive(false);
        connectUI.SetActive(false);
        NetworkManagerClient.Singleton.Connect();
    }

    public void HostClicked() {
        usernameField.interactable = false;
        hostUI.SetActive(false);
        connectUI.SetActive(false);
        NetworkManager.Singleton.CreateServer();
    }

    public void BackToMain() {
        usernameField.interactable = true;
        connectUI.SetActive(true);
    }

    public void SendName() {
        Message message = Message.Create(MessageSendMode.reliable,(ushort) ClientToServerId.name);
        message.AddString(usernameField.text);

        NetworkManagerClient.Singleton.Client.Send(message);
    }

}
