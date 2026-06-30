using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class GameManager : MonoBehaviour
{
  
    public static GameManager Instance { get; private set; }

   
    public enum GameState { Iniciando, MenuPrincipal, Gameplay }
    private GameState currentState;

    [Header("Configurações")]
    [SerializeField] private string splashSceneName = "Splash";
    [SerializeField] private string mainMenuSceneName = "MenuPrincipal";
    [SerializeField] private string gameplaySceneName = "GetStarted_Scene";
    [SerializeField] private string guiSceneName = "GUI"; 

    private void Awake()
    {
      
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
     
        if (SceneManager.GetActiveScene().name == "_Boot")
        {
            LoadSceneWithState(splashSceneName, GameState.Iniciando);
        }
    }


    public void ChangeState(GameState newState)
    {
        currentState = newState;
        Debug.Log($"<color=cyan>[GameManager]</color> Estado alterado para: {currentState}");

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

    
    public void LoadSceneWithState(string sceneName, GameState newState)
    {
        ChangeState(newState);

       
        if (newState == GameState.Gameplay)
        {
            StartCoroutine(LoadGameplayWithGUI(sceneName));
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    
    private IEnumerator LoadGameplayWithGUI(string gameplayScene)
    {
       
        AsyncOperation loadGameplay = SceneManager.LoadSceneAsync(gameplayScene, LoadSceneMode.Single);
        
       
        while (!loadGameplay.isDone)
        {
            yield return null;
        }

        
        if (!SceneManager.GetSceneByName(guiSceneName).isLoaded)
        {
            AsyncOperation loadGUI = SceneManager.LoadSceneAsync(guiSceneName, LoadSceneMode.Additive);
            
      
            while (!loadGUI.isDone)
            {
                yield return null;
            }
        }
        
        Debug.Log("<color=green>[GameManager]</color> Gameplay e GUI carregadas com sucesso!");
    }

 
    public void SetupPlayerInput(PlayerInput playerInput)
    {
        if (playerInput != null)
        {
            Debug.Log("Input alocado ao jogador pelo GameManager.");
            playerInput.enabled = (currentState == GameState.Gameplay);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }

    public void MudarCena(string menuprincipal)
    {
        throw new System.NotImplementedException();
    }
}