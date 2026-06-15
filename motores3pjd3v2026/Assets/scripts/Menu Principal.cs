using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void IniciarJogo()
    {
        
        GameManager.Instance.MudarCena("GetStarted_Scene");
    }
}