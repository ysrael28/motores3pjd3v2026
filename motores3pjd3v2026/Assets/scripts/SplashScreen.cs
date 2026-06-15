using UnityEngine;
using System.Collections;

public class SplashTimer : MonoBehaviour
{
    void Start()
    {
       
        Invoke("IrParaMenu", 2f);
    }

    void IrParaMenu()
    {
       
        GameManager.Instance.MudarCena("MenuPrincipal");
    }
}