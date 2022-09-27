using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public NetworkVariable<FixedString32Bytes> PlayerName = new NetworkVariable<FixedString32Bytes>(default,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);

    public Fleet Fleet;

    public FleetController Controller;
    
    private void Start()
    {
        Fleet = gameObject.GetComponent<Fleet>();
        Fleet.battleField = gameObject.GetComponent<BattleField>();
        Controller = gameObject.GetComponent<FleetController>();
        if (IsLocalPlayer)
        {
            PlayerName.Value =InputPlayerDataClass.PlayerNameFromInputLabel;
 
        }

        if (!IsLocalPlayer)
        {
            gameObject.AddComponent<Enemy>();
        }
    }
    
}
