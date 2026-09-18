using UnityEngine;
using StarterAssets; // Namespace necessário para aceder ao ThirdPersonController

public class PlayerMoedaCollector : MonoBehaviour
{
    // Identificador atribuído pelo GeradorPlayer (0 = P1, 1 = P2)
    public int playerIndex = 0;

    [Header("Aumento de Velocidade")]
    [SerializeField] private float incrementoVelocidade = 0.5f; // Quanto a velocidade aumenta por moeda
    [SerializeField] private float velocidadeMaxima = 12.0f;     // Limite para o robô

    private int moedaCount = 0;
    private ThirdPersonController controller;

    private void Awake()
    {
        controller = GetComponent<ThirdPersonController>();
    }

    // Funciona se a moeda tiver "Is Trigger" marcado no Collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            ProcessarColetaMoeda(other.gameObject);
        }
    }

    // Funciona se a moeda tiver colisão física normal (Is Trigger desmarcado)
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Coin"))
        {
            ProcessarColetaMoeda(hit.gameObject);
        }
    }

    private void ProcessarColetaMoeda(GameObject moedaObj)
    {
        moedaCount++;

        // 1. Aumenta a velocidade de movimento
        AumentarVelocidade();

        // 2. Atualiza o contador de moedas na UI através do GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegistrarMoedaColetada(playerIndex, moedaCount);
        }

        // 3. Notifica o evento do Observer Manager
        PlayerObserverManager.NotifyMoedaCollected(moedaCount);

        // 4. Destrói o objeto da moeda
        Destroy(moedaObj);
    }

    private void AumentarVelocidade()
    {
        if (controller != null)
        {
            controller.MoveSpeed = Mathf.Min(controller.MoveSpeed + incrementoVelocidade, velocidadeMaxima);
            controller.SprintSpeed = Mathf.Min(controller.SprintSpeed + incrementoVelocidade, velocidadeMaxima * 1.5f);
        }
    }
}