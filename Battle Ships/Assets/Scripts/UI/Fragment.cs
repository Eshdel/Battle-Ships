using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Fragment : MonoBehaviour
{
   public View [] _views;
   
   public bool ShowFragmentName = true;
    
   public virtual void Open() 
   {
      this.gameObject.SetActive(true);
      foreach (var view in _views)
      {
         view.Show();
      }
   }

   public virtual void Close()
   {
      this.gameObject.SetActive(false);
      foreach (var view in _views)
      {
         view.Hide();
      }
   }

   public abstract void SetActionOnView();

   public virtual void OnStart() {}

   private void Start()
   {
      OnStart();
      SetActionOnView();
   }
}
