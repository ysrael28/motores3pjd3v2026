using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

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

        // Instancia os dois jogadores associados ao teclado
        PlayerInput p1 = inputManager.JoinPlayer(
            playerIndex: 0,
            splitScreenIndex: -1,
            controlScheme: "KeyboardWASD",
            pairWithDevice: Keyboard.current
        );

        PlayerInput p2 = inputManager.JoinPlayer(
            playerIndex: 1,
            splitScreenIndex: -1,
            controlScheme: "KeyboardArrows",
            pairWithDevice: Keyboard.current
        );

        ConfigurarJogador(p1, 0, "KeyboardWASD", spawnPointP1, OutputChannels.Channel01);
        ConfigurarJogador(p2, 1, "KeyboardArrows", spawnPointP2, OutputChannels.Channel02);
    }

    private void ConfigurarJogador(PlayerInput player, int index, string schemeName, Transform spawn, OutputChannels channel)
    {
        if (player == null) return;
        
        PlayerMoedaCollector collector = player.GetComponent<PlayerMoedaCollector>();
        if (collector != null)
        {
            collector.playerIndex = index;
        }
        
        if (spawn != null)
        {
            player.transform.position = spawn.position;
            player.transform.rotation = spawn.rotation;
        }
        
        try
        {
            player.SwitchCurrentControlScheme(schemeName, Keyboard.current);
            player.defaultControlScheme = schemeName;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"[GeradorPlayer] Falha ao aplicar ControlScheme {schemeName}: {e.Message}");
        }
        
        CinemachineCamera vcam = player.GetComponentInChildren<CinemachineCamera>();
        if (vcam != null)
        {
            vcam.OutputChannel = channel;

            Transform targetPoint = player.transform.Find("CameraTargetPoint");
            if (targetPoint == null)
            {
                GameObject newTarget = new GameObject("CameraTargetPoint");
                newTarget.transform.SetParent(player.transform);
                newTarget.transform.localPosition = new Vector3(0f, 1.5f, 0f);
                newTarget.transform.localRotation = Quaternion.identity;
                targetPoint = newTarget.transform;
            }

            vcam.Target.TrackingTarget = targetPoint;
            vcam.Target.LookAtTarget = targetPoint;

            var thirdPersonFollow = vcam.GetComponent<CinemachineThirdPersonFollow>();
            if (thirdPersonFollow != null)
            {
                thirdPersonFollow.ShoulderOffset = new Vector3(0f, 0.5f, -3.5f);
                thirdPersonFollow.VerticalArmLength = 0.4f;
                thirdPersonFollow.CameraSide = 0.5f;
                thirdPersonFollow.CameraDistance = 4f;
            }
        }

        CinemachineBrain brain = player.GetComponentInChildren<CinemachineBrain>();
        if (brain != null)
        {
            brain.ChannelMask = channel;
        }
    }
}