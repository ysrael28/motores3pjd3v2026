using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class RobotMovement : MonoBehaviour
{
    private StarterAssetsInputs starterInputs;

    private void Awake()
    {
        // Pega o componente StarterAssetsInputs na raiz
        starterInputs = GetComponent<StarterAssetsInputs>();
    }

    // Corrigido para receber InputValue (padrão do Send Messages)
    public void OnMove(InputValue value)
    {
        if (starterInputs != null)
        {
            // Repassa o movimento para o Starter Assets
            starterInputs.MoveInput(value.Get<Vector2>());
        }
    }
}