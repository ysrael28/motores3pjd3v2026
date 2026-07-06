using UnityEngine;
using UnityEngine.InputSystem;

public class playerinteract : MonoBehaviour
{
  
  
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            interactOM.Interact();
        }
    }
}
