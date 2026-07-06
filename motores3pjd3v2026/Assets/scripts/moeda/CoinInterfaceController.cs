using UnityEngine;
using TMPro;
using System;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;

    private void OnEnable()
    {
        PlayerObserverManager.OnCoinCollected += UpdateCoins;
    }

    private void OnDisable()
    {
        PlayerObserverManager.OnCoinCollected -= UpdateCoins;
    }

    private void UpdateCoins(int totalCoins)
    {
        if (coinsText == null)
        {
            return;
        } 
        else 
        {
            coinsText.text = "Moedas: " + coins.toString();
        }
      
    }
}