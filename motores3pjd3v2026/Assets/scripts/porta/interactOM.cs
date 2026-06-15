using System;
using UnityEngine;

public static class interactOM
{
    public static event Action OnInteract;

    public static void Interact()
    {
        OnInteract?.Invoke();
    }
}
