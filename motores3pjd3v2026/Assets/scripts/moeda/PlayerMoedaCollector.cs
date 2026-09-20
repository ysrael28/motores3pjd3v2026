using UnityEngine;
using StarterAssets;

public class PlayerMoedaCollector : MonoBehaviour
{
    public int playerIndex = 0; 

    [Header("Aumento de Velocidade")]
    [SerializeField] private float incrementoVelocidade = 0.5f; 
    [SerializeField] private float velocidadeMaxima = 12.0f;     

    private int moedaCount = 0;
    private ThirdPersonController controller;

    private void Awake()
    {
        controller = GetComponent<ThirdPersonController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            ProcessarColetaMoeda(other.gameObject);
        }
    }

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

        AumentarVelocidade();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegistrarMoedaColetada(playerIndex, moedaCount);
        }
        else
        {
            Debug.LogError("[Collector] ERRO: GameManager.Instance está NULO!");
        }

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