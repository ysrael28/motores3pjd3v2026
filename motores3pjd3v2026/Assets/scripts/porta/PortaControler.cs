using System;
using UnityEngine;

public class PortaControler : MonoBehaviour
{
   private Animator protaAnim;
   private bool isOpen;
   private bool isInteractable;

   private void Start()
   {
      var portaAnim = GetComponentInChildren<Animator>();
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player") && !isInteractable)
      {
         isInteractable = true;
         interactOM.OnInteract += AbreFechaPorta;
      }
      
      
   }

   private void AbreFechaPorta()
   {
      throw new NotImplementedException();
   }
}
