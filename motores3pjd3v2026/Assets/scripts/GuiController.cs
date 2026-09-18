using UnityEngine;
using TMPro;

public class GuiController : MonoBehaviour
{
    [Header("Textos de Score (Moedas)")]
    public TextMeshProUGUI p1ScoreText;
    public TextMeshProUGUI p2ScoreText;

    [Header("Painel de Vitória")]
    public GameObject winPanel;
    public TextMeshProUGUI winText;

    private void Start()
    {
        // Garante que o painel de vitória começa escondido
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        // Regista automaticamente este controlador no GameManager assim que a cena GUI carrega
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegistrarUI(this);
        }
    }

    public void MostrarVitoria(string mensagem)
    {
        if (winText != null)
        {
            winText.text = mensagem;
        }

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }

    public void BotaoReiniciar()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RequestSceneChange("GetStarted_Scene");
        }
    }

    public void BotaoMenuPrincipal()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadSceneWithState("MenuPrincipal", GameManager.GameState.MenuPrincipal);
        }
    }
}