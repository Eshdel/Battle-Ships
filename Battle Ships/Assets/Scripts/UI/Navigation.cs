using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
   private bool backButtonEnabled = true;
   private Stack<Fragment> _stack;

   [SerializeField] private GameObject _backButton;
   [SerializeField] private Fragment _firstFragment;
   public void Add(Fragment fragment)
   {
      if (_stack.Count > 0) 
         _stack.Peek().Close();
      
      _stack.Push(fragment);
      fragment.Open();

      if (backButtonEnabled && _stack.Count > 1) 
         _backButton.SetActive(true);
   }

   public void GoBack()
   {
      Fragment closedView = _stack.Pop();

      if (_stack.Count == 1) {
         _backButton.SetActive(false);
      }
      
      _stack.Peek().Open();
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
