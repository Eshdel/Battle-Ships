using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

using UnityEngine.UI;


public class NewGameFragment : Fragment
{
    [SerializeField] private Fragment multiplayerFragment;
    
    [SerializeField] private Button singleGameButton;
    
    [SerializeField] private Button multiplayerGameButton;

    public override void SetActionOnView()
    {
        singleGameButton.onClick.AddListener(null);
        multiplayerGameButton.onClick.AddListener(OpenMultiPlayerFragment);
    }

    private void OpenMultiPlayerFragment()
    {
        UIManager.Singleton.mainMenuNavigation.Add(multiplayerFragment); 
    }


    
}
