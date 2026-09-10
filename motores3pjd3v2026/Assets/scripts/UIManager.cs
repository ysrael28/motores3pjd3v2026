using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Textos do Placar")]
    public TextMeshProUGUI p1ScoreText;
    public TextMeshProUGUI p2ScoreText;
    public TextMeshProUGUI totalRemainingText;

    [Header("Painel de Vitória")]
    public GameObject winPanel;
    public TextMeshProUGUI winText;

    private void Start()
    {
        if (winPanel != null)
            winPanel.SetActive(false);

        // Quando a cena GUI é criada aditivamente pelo GameManager,
        // ela se "apresenta" para ele registrar as referências dos textos.
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegistrarUI(this);
        }
    }
}