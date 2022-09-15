using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FirtstFragment : Fragment
{
    [SerializeField] private Fragment subView;
    
    public override void SetActionOnView()
    {
        _views[0].Action = () => {OpenSecondeFragment();};
    }
    
    private void OpenSecondeFragment() {
        UIManager.Singleton.mainMenuNavigation.Add(subView);
    }
}
