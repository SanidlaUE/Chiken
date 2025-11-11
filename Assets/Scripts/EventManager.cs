using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
   public static event Action Get;
   public static event Action Select;
   public static event Action Back;

   public static void GetSkin()
   {
      Get?.Invoke();
   }
   public static void SelectSkin()
   {
      Select?.Invoke();
   }
   public static void BackToMain()
   {
      Back?.Invoke();
   }
}

