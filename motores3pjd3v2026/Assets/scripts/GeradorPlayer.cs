using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine; // Dependência do Cinemachine 3

public class GeradorPlayer : MonoBehaviour
{
    [SerializeField] private PlayerInputManager inputManager;
    [SerializeField] private Transform spawnPointP1;
    [SerializeField] private Transform spawnPointP2;

    private void Awake()
    {
        if (inputManager == null)
            inputManager = GetComponent<PlayerInputManager>();
    }

    private void Start()
    {
        InstanciarJogadores();
    }

    public void InstanciarJogadores()
    {
        if (inputManager == null || inputManager.playerPrefab == null) return;

        // Player 1: Teclado WASD
        PlayerInput p1 = inputManager.JoinPlayer(
            playerIndex: 0,
            splitScreenIndex: -1,
            controlScheme: "KeyboardWASD", // Corrigido para bater com o Input Actions
            pairWithDevice: Keyboard.current
        );

        // Player 2: Teclado Setas
        PlayerInput p2 = inputManager.JoinPlayer(
            playerIndex: 1,
            splitScreenIndex: -1,
            controlScheme: "KeyboardArrows", // Corrigido para bater com o Input Actions
            pairWithDevice: Keyboard.current
        );

        // Configura Spawn, Coletor, Input e Câmera para o P1
        ConfigurarJogador(p1, 0, spawnPointP1, OutputChannels.Channel01);

        // Configura Spawn, Coletor, Input e Câmera para o P2
        ConfigurarJogador(p2, 1, spawnPointP2, OutputChannels.Channel02);
    }

    private void ConfigurarJogador(PlayerInput player, int index, Transform spawn, OutputChannels channel)
    {
        if (player == null) return;

        // 1. Posicionamento
        if (spawn != null)
        {
            player.transform.position = spawn.position;
            player.transform.rotation = spawn.rotation;
        }

        // 2. Identificador de Moedas (0 = P1, 1 = P2)
        PlayerMoedaCollector collector = player.GetComponent<PlayerMoedaCollector>();
        if (collector != null)
        {
            collector.playerIndex = index;
        }

        // 3. Ativa o Input pelo GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AllocatePlayerInput(player);
        }

        // 4. Isolamento da Câmera Virtual do Robô (Cinemachine)
        CinemachineCamera vcam = player.GetComponentInChildren<CinemachineCamera>();
        if (vcam != null)
        {
            vcam.OutputChannel = channel;
        }

        // 5. Isolamento do Receptor da Câmera (CinemachineBrain)
        CinemachineBrain brain = player.GetComponentInChildren<CinemachineBrain>();
        if (brain != null)
        {
            brain.ChannelMask = channel;
        }
    }
}
