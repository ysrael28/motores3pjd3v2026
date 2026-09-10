using UnityEngine;
using StarterAssets; // Namespace necessário para acessar o ThirdPersonController

public class PlayerMoedaCollector : MonoBehaviour
{
    // Identificador atribuído pelo GeradorPlayer (0 = P1, 1 = P2)
    public int playerIndex = 0; 

    [Header("Aumento de Velocidade")]
    [SerializeField] private float incrementoVelocidade = 0.5f; // Quanto a velocidade aumenta por moeda
    [SerializeField] private float velocidadeMaxima = 12.0f;     // Limite para o robô não ficar incontrolável

    private int moedaCount = 0;
    private ThirdPersonController controller;

    private void Awake()
    {
        // Pega o componente do ThirdPersonController presente no próprio robô
        controller = GetComponent<ThirdPersonController>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Coin"))
        {
            moedaCount++;

            // 1. Aumenta a velocidade do robô
            AumentarVelocidade();

            // 2. Atualiza a pontuação no GameManager persistente
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AdicionarPontuacao(playerIndex);
            }

            // 3. Dispara o evento para atualizar a interface (UI)
            PlayerObserverManager.NotifyMoedaCollected(moedaCount);

            // 4. Destrói o objeto da moeda
            Destroy(hit.gameObject);
        }
    }

    private void AumentarVelocidade()
    {
        if (controller != null)
        {
            // Aumenta a velocidade e aplica o limite máximo definido
            controller.MoveSpeed = Mathf.Min(controller.MoveSpeed + incrementoVelocidade, velocidadeMaxima);
            
            // Opcional: Aumenta a velocidade de corrida proporcionalmente (SprintSpeed)
            controller.SprintSpeed = Mathf.Min(controller.SprintSpeed + incrementoVelocidade, velocidadeMaxima * 1.5f);
        }
    }
}