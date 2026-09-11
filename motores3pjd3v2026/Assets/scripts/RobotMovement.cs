using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class RobotMovement : MonoBehaviour
{
    private StarterAssetsInputs starterInputs;

    private void Awake()
    {
        starterInputs = GetComponent<StarterAssetsInputs>();
    }

    // Assinatura com InputValue para compatibilidade com o comportamento 'Send Messages' do PlayerInput
    public void OnMove(InputValue value)
    {
        if (starterInputs != null)
        {
            starterInputs.MoveInput(value.Get<Vector2>());
        }
    }
}