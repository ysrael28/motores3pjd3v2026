using UnityEngine;
using UnityEngine.InputSystem;

public class playerinteract : MonoBehaviour
{
  
    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            interactOM.Interact();
        }
    }
}
