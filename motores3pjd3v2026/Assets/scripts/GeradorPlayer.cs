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

        // Associa ambos os jogadores ao mesmo teclado físico
        PlayerInput p1 = inputManager.JoinPlayer(
            playerIndex: 0,
            splitScreenIndex: -1,
            controlScheme: null,
            pairWithDevice: Keyboard.current
        );

        PlayerInput p2 = inputManager.JoinPlayer(
            playerIndex: 1,
            splitScreenIndex: -1,
            controlScheme: null,
            pairWithDevice: Keyboard.current
        );

        ConfigurarJogador(p1, 0, "KeyboardWASD", spawnPointP1, OutputChannels.Channel01);
        ConfigurarJogador(p2, 1, "KeyboardArrows", spawnPointP2, OutputChannels.Channel02);
    }

    private void ConfigurarJogador(PlayerInput player, int index, string schemeName, Transform spawn, OutputChannels channel)
    {
        if (player == null) return;

        player.SwitchCurrentControlScheme(schemeName, Keyboard.current);
        player.SwitchCurrentActionMap("Player");

        if (spawn != null)
        {
            player.transform.position = spawn.position;
            player.transform.rotation = spawn.rotation;
        }

        PlayerMoedaCollector collector = player.GetComponent<PlayerMoedaCollector>();
        if (collector != null)
        {
            collector.playerIndex = index;
        }

        CinemachineCamera vcam = player.GetComponentInChildren<CinemachineCamera>();
        if (vcam != null)
        {
            vcam.OutputChannel = channel;
        }

        CinemachineBrain brain = player.GetComponentInChildren<CinemachineBrain>();
        if (brain != null)
        {
            brain.ChannelMask = channel;
        }
    }
}