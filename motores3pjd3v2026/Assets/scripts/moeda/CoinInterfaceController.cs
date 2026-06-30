using UnityEngine;
using TMPro;

public class CoinInterfaceController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    private void Start()
    {
        if (coinText != null) coinText.text = "Moedas: 0";
    }

    private void OnEnable()
    {
        PlayerObserverManager.OnCoinCollected += UpdateCoinDisplay;
        Debug.Log("<color=green>[Interface]</color> Conectada ao Observer e esperando moedas...");
    }

    private void OnDisable()
    {
        PlayerObserverManager.OnCoinCollected -= UpdateCoinDisplay;
    }

    private void UpdateCoinDisplay(int currentCoins)
    {
        Debug.Log($"<color=green>[Interface]</color> Mensagem recebida! Atualizando texto para: {currentCoins}");
        if (coinText != null)
        {
            coinText.text = $"Moedas: {currentCoins}";
        }
    }
}