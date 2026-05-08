using UnityEngine;
using System.Collections; // Necessário para o IEnumerator

public class SplashScreen : MonoBehaviour
{
    private IEnumerator Start()
    {
        // Espera 2 segundos
        yield return new WaitForSeconds(2f);

        // Verifica se o GameManager existe antes de chamar para evitar erros
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadSceneWithState("MenuPrincipal", GameManager.GameState.MenuPrincipal);
        }
        else
        {
            Debug.LogError("GameManager não encontrado! Certifique-se de começar pela cena _Boot.");
        }
    }
}