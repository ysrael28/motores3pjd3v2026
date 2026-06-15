using UnityEngine;
using UnityEngine.SceneManagement; 


public enum GameState { Iniciando, MenuPrincipal, Gameplay }

public class GameManager : MonoBehaviour
{
   
    public static GameManager Instance;

    public GameState estadoAtual;

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
        }
    }

    private void Start()
    {
        
        MudarEstado(GameState.Iniciando);
        MudarCena("Splash");
    }

    
    public void MudarCena(string nomeCena)
    {
        Debug.Log("GameManager: Carregando cena " + nomeCena);
        SceneManager.LoadScene(nomeCena);

        
        if (nomeCena == "MenuPrincipal") MudarEstado(GameState.MenuPrincipal);
        if (nomeCena == "GetStarted_Scene") MudarEstado(GameState.Gameplay);
    }

    public void MudarEstado(GameState novoEstado)
    {
        estadoAtual = novoEstado;
        Debug.Log("ESTADO ATUAL: " + estadoAtual); 
    }

    public void SairDoJogo()
    {
        Application.Quit();
        Debug.Log("O jogo fecharia agora (n�o funciona no Editor)");
    }
}