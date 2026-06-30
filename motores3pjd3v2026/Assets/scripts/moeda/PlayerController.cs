using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int coinCount = 0;

    public void AddCoin()
    {
        coinCount++;
        Debug.Log($"<color=yellow>[Player]</color> Coletei uma moeda! Total: {coinCount}. Avisando o Observer Manager...");
        
        // Notifica o gerenciador de eventos
        PlayerObserverManager.TriggerCoinCollected(coinCount);
    }
}