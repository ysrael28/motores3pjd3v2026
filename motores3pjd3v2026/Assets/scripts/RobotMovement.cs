using UnityEngine;
using UnityEngine.InputSystem;

public class RobotMovement : MonoBehaviour
{
    private Vector2 moveInput;

    // Método disparado pelo evento OnMove do PlayerInput
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Aplica o movimento usando o Vector2 recebido
        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);
        // Exemplo: rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
}