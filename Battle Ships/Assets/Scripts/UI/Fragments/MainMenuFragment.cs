using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenuFragment : Fragment
{
    [SerializeField] private Fragment subView;
    
    public override void SetActionOnView()
    {
        _views[0].Action = () => OpenSecondeFragment();
        _views[4].Action = () =>  Application.Quit(); 
    }
    
    private void OpenSecondeFragment() 
    {
        UIManager.Singleton.mainMenuNavigation.Add(subView);
    }
    
}
