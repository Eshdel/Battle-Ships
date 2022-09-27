using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class FleetController : NetworkBehaviour
{
    public NetworkVariable<bool> IsYourTurn = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public NetworkVariable<Vector3> tryAttackPos = new NetworkVariable<Vector3>(default,
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    // ReSharper disable Unity.PerformanceAnalysis
    private void MakeTurn()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (!IsOwner)
        {
            Debug.Log("Not owner");                                                                                                                                                          
            return;
        }
     
        if (Input.GetButtonDown("Fire1"))
        {
         
            if (Physics.Raycast(ray))
            {
                tryAttackPos.Value = ray.GetPoint(100);
                Debug.Log("Point   " + ray.GetPoint(100));
            }   
        }
        
        if (Physics.Raycast(ray, out hit, 100))
        {

            var offset = hit.collider.gameObject.GetComponentInParent<OwnerField>().player.Fleet.battleField.Offset;
            var point = ray.GetPoint(100);
            Debug.Log("Point  "+ Math.Round(Mathf.Abs(point.z)) + " | " + Math.Round(Math.Abs(point.x - (offset.x + offset.y))));
            
        }
    }

    private void Update()
    {
        if (IsYourTurn.Value == true)
        {
            MakeTurn();    
        }
    }
}
