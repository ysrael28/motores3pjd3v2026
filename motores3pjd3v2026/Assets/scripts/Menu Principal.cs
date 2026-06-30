using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void IniciarJogo()
    {
      
        GameManager.Instance.LoadSceneWithState("GetStarted_Scene", GameManager.GameState.Gameplay);
    }
}