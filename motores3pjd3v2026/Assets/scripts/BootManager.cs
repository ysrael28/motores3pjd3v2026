using System.Collections;
using UnityEngine;

public class BootManager : MonoBehaviour
{
    // Corrigido para carregar a cena "Splash" (ou "MenuPrincipal") em vez de "Menu"
    [SerializeField] private string proximaCena = "Splash"; 
    [SerializeField] private float tempoEspera = 0.5f;

    private IEnumerator Start()
    {
        yield return null;

        if (tempoEspera > 0)
        {
            yield return new WaitForSeconds(tempoEspera);
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.RequestSceneChange(proximaCena);
        }
        else
        {
            Debug.LogError("GameManager não encontrado na cena _Boot!");
        }
    }
}