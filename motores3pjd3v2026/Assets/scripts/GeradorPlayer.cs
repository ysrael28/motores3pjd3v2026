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

        // Instancia e conecta P1 e P2 ao teclado
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

        // 1. GARANTIA: Define o index do Player no Collector PRIMEIRO para a UI funcionar
        PlayerMoedaCollector collector = player.GetComponent<PlayerMoedaCollector>();
        if (collector != null)
        {
            collector.playerIndex = index;
            Debug.Log($"[GeradorPlayer] Jogador {index + 1} configurado com sucesso! (Index: {index})");
        }
        else
        {
            Debug.LogError($"[GeradorPlayer] Script PlayerMoedaCollector NÃO encontrado no Prefab do Player {index + 1}!");
        }

        // 2. Posiciona no Spawn Point
        if (spawn != null)
        {
            player.transform.position = spawn.position;
            player.transform.rotation = spawn.rotation;
        }

        // 3. Configura esquemas de Input sem travar o código se o nome do mapa for diferente
        try
        {
            player.SwitchCurrentControlScheme(schemeName, Keyboard.current);

            if (player.actions != null && player.actions.FindActionMap("Player") != null)
            {
                player.SwitchCurrentActionMap("Player");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"[GeradorPlayer] Aviso de Input no Player {index + 1}: {e.Message}");
        }

        // 4. Configuração de Câmera (Alvo a 1.5m de altura no peito do robô)
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