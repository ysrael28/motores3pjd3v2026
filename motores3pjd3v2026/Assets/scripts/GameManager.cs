using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Implementação Singleton
    public static GameManager Instance { get; private set; }

    // Enum de Estados
    public enum GameState { Iniciando, MenuPrincipal, Gameplay }
    private GameState currentState;

    [Header("Configurações")]
    [SerializeField] private string splashSceneName = "Splash";
    [SerializeField] private string mainMenuSceneName = "MenuPrincipal";
    [SerializeField] private string gameplaySceneName = "GetStarted_Scene";

    private void Awake()
    {
        // Lógica de Singleton para persistência entre cenas
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        ChangeState(GameState.Iniciando);
    }

    private void Start()
    {
        // Se começarmos na cena de Boot, vamos para a Splash
        if (SceneManager.GetActiveScene().name == "_Boot")
        {
            LoadSceneWithState(splashSceneName, GameState.Iniciando);
        }
    }

    // Máquina de estados simplificada
    public void ChangeState(GameState newState)
    {
        currentState = newState;
        Debug.Log($"<color=cyan>[GameManager]</color> Estado alterado para: {currentState}");

        // Lógica disparada na mudança de estado (ex: liberar input apenas no Gameplay)
        switch (currentState)
        {
            case GameState.Iniciando:
                break;
            case GameState.MenuPrincipal:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case GameState.Gameplay:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
        }
    }

    // Centralizador de carregamento de cenas
    public void LoadSceneWithState(string sceneName, GameState newState)
    {
        // Aqui você pode adicionar verificações de segurança
        // Ex: Não carregar Gameplay se o estado atual for Iniciando sem passar pelo Menu
        
        ChangeState(newState);
        SceneManager.LoadScene(sceneName);
    }

    // Alocação de Input (Exemplo para Single Player)
    public void SetupPlayerInput(PlayerInput playerInput)
    {
        if (playerInput != null)
        {
            Debug.Log("Input alocado ao jogador pelo GameManager.");
            // Aqui você poderia definir qual esquema de controle usar
            playerInput.enabled = (currentState == GameState.Gameplay);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}