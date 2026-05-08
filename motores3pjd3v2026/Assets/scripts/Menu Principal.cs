using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void ClickIniciar()
    {
        // Chama o Singleton sem precisar que o botão o conheça diretamente
        GameManager.Instance.LoadSceneWithState("GetStarted_Scene", GameManager.GameState.Gameplay);
    }

    public void ClickSair()
    {
        GameManager.Instance.QuitGame();
    }
}