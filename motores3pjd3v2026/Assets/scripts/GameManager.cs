using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState
    {
        Splash,
        MenuPrincipal,
        Gameplay,
        GameOver
    }

    [Header("Estado Atual")]
    public GameState estadoAtual = GameState.Splash;

    [Header("Configuração Inicial (Boot)")]
    [SerializeField] private string primeiraCena = "Splash";
    [SerializeField] private float tempoEsperaBoot = 0.5f;

    [Header("Pontuação e Estrelas (Condição de Vitória)")]
    public int p1Score = 0;
    public int p2Score = 0;
    public int totalEstrelasNaCena = 0;
    public int estrelasColetadasTotal = 0;

    [Header("Moedas (Apenas Velocidade e Interface)")]
    public int p1Moedas = 0;
    public int p2Moedas = 0;

    private GuiController guiController;

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
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private IEnumerator Start()
    {
        if (SceneManager.GetActiveScene().name == "_Boot")
        {
            if (tempoEsperaBoot > 0)
            {
                yield return new WaitForSeconds(tempoEsperaBoot);
            }

            RequestSceneChange(primeiraCena);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Splash")
        {
            estadoAtual = GameState.Splash;
        }
        else if (scene.name == "MenuPrincipal" || scene.name == "Menu")
        {
            estadoAtual = GameState.MenuPrincipal;
        }
        else if (scene.name == "GetStarted_Scene" || scene.name == "Jogo")
        {
            estadoAtual = GameState.Gameplay;
            DetectarEstrelasNaCena();
        }

        AtualizarUI();
    }

    public void DetectarEstrelasNaCena()
    {
        Pickup[] estrelasEncontradas = FindObjectsByType<Pickup>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        totalEstrelasNaCena = estrelasEncontradas.Length;

        if (totalEstrelasNaCena == 0)
        {
            GameObject[] estrelasPorTag = GameObject.FindGameObjectsWithTag("Estrela");
            totalEstrelasNaCena = estrelasPorTag.Length;
        }
    }

    public void RegistrarUI(GuiController gui)
    {
        guiController = gui;
        AtualizarUI();
    }

    public void LoadSceneWithState(string sceneName, GameState state)
    {
        estadoAtual = state;
        RequestSceneChange(sceneName);
    }

    public void RequestSceneChange(string nomeDaCena)
    {
        StartCoroutine(CarregarCenasProcesso(nomeDaCena));
    }

    private IEnumerator CarregarCenasProcesso(string nomeDaCena)
    {
        p1Score = 0;
        p2Score = 0;
        p1Moedas = 0;
        p2Moedas = 0;
        estrelasColetadasTotal = 0;
        guiController = null;

        AsyncOperation opGameplay = SceneManager.LoadSceneAsync(nomeDaCena, LoadSceneMode.Single);
        while (!opGameplay.isDone)
        {
            yield return null;
        }

        if (nomeDaCena == "GetStarted_Scene" || nomeDaCena == "Jogo")
        {
            AsyncOperation opGUI = SceneManager.LoadSceneAsync("GUI", LoadSceneMode.Additive);
            while (!opGUI.isDone)
            {
                yield return null;
            }
        }
    }

    // Registra a coleta de MOEDAS (Apenas atualiza a UI sem encerrar a partida)
    public void RegistrarMoedaColetada(int playerIndex, int totalMoedasPlayer)
    {
        if (playerIndex == 0) p1Moedas = totalMoedasPlayer;
        else if (playerIndex == 1) p2Moedas = totalMoedasPlayer;

        AtualizarUI();
    }

    // Compatibilidade mantida para scripts antigos
    public void AdicionarPontuacao(int playerIndex)
    {
        AdicionarEstrela(playerIndex);
    }

    // Registra a coleta de ESTRELAS (Única contagem que pode declarar vitória)
    public void AdicionarEstrela(int playerIndex)
    {
        estrelasColetadasTotal++;

        if (playerIndex == 0)
        {
            p1Score++;
        }
        else if (playerIndex == 1)
        {
            p2Score++;
        }

        AtualizarUI();

        if (totalEstrelasNaCena > 0 && estrelasColetadasTotal >= totalEstrelasNaCena)
        {
            ExibirTelaDeVitoria();
        }
    }

    public void AtualizarUI()
    {
        if (guiController == null) return;

        if (guiController.p1ScoreText != null)
            guiController.p1ScoreText.text = $"P1 Moedas: {p1Moedas}";

        if (guiController.p2ScoreText != null)
            guiController.p2ScoreText.text = $"P2 Moedas: {p2Moedas}";
    }

    private void ExibirTelaDeVitoria()
    {
        if (guiController == null) return;

        string mensagemResultado;

        if (p1Score > p2Score)
            mensagemResultado = "PLAYER 1 VENCEU!";
        else if (p2Score > p1Score)
            mensagemResultado = "PLAYER 2 VENCEU!";
        else
            mensagemResultado = "EMPATE!";

        guiController.MostrarVitoria(mensagemResultado);
    }

    public void AllocatePlayerInput(PlayerInput player)
    {
        if (player == null) return;

        var actionMap = player.actions.FindActionMap("Player");
        if (actionMap != null)
        {
            player.SwitchCurrentActionMap("Player");
        }
        else
        {
            player.currentActionMap?.Enable();
        }
    }
}