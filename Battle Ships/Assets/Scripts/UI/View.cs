using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    public Action Action;
    
    public  bool isVisable { get; private set; }
    
    public void Show() {
        this.gameObject.SetActive(true);
        isVisable = true;
    }

    public void Hide() {
        this.gameObject.SetActive(false);
        isVisable = false;
    }

    public void OnClick() {
        
        if (Action != null) 
            Action.Invoke();
    }
}
