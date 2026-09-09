using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Arraste o Prefab do Robô no campo Player Prefab do GeradorPlayer!");
            return;
        }

        // Instancia o Jogador 1 associado ao esquema Keyboard_P1 (WASD)
        PlayerInput p1 = PlayerInput.Instantiate(
            playerPrefab,
            playerIndex: 0,
            controlScheme: "Keyboard_P1",
            pairWithDevice: Keyboard.current
        );

        // Instancia o Jogador 2 associado ao esquema Keyboard_P2 (Setas)
        PlayerInput p2 = PlayerInput.Instantiate(
            playerPrefab,
            playerIndex: 1,
            controlScheme: "Keyboard_P2",
            pairWithDevice: Keyboard.current
        );
    }
}