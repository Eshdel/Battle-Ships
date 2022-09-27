using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
   private bool backButtonEnabled = true;
   private Stack<Fragment> _stack;

   [SerializeField] private GameObject _backButton;
   
   [SerializeField] private Fragment _firstFragment;

   [SerializeField] private Text _fragmentName;

   public void Add(Fragment fragment)
   {
      if (_stack.Count > 0) 
         _stack.Peek().Close();
      
      _stack.Push(fragment);
      fragment.Open();

      if (backButtonEnabled && _stack.Count > 1) 
         _backButton.SetActive(true);
      
      if (fragment.ShowFragmentName)
      {
         _fragmentName.gameObject.SetActive(true);
         _fragmentName.text = fragment.name;
      }
   }

   public void GoBack()
   {
      Fragment closedView = _stack.Pop();

      if (_stack.Count == 1) {
         _backButton.SetActive(false);
      }
     
      _fragmentName.gameObject.SetActive(false);
      _stack.Peek().Open();
    
      if (_stack.Peek().ShowFragmentName)
      {
         _fragmentName.gameObject.SetActive(true);
         _fragmentName.text = _stack.Peek().name;
      }
      
      closedView.Close();
   }

   public void ClearStack()
   {
      foreach (Fragment fragment in _stack)
      {
         fragment.Close();
      }

      _stack.Clear();

      _backButton.SetActive(false);
   }

   public void HideBackButton()
   {
      backButtonEnabled = false;
      _backButton.SetActive(false);
   }

   public void ShowBackButton()
   {
      backButtonEnabled = true;
      _backButton.SetActive(true);
   }

   private void Start()
   {
      _stack = new Stack<Fragment>();
      Add(_firstFragment);
   }
}
