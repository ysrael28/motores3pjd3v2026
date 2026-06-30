using UnityEngine;

public class SplashTimer : MonoBehaviour
{
    void Start()
    {

        Invoke("IrParaMenu", 2f);
    }

    void IrParaMenu()
    {
     
        GameManager.Instance.LoadSceneWithState("MenuPrincipal", GameManager.GameState.MenuPrincipal);
    }
}