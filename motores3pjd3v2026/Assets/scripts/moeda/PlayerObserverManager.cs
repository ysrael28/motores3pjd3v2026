using System;

public static class PlayerObserverManager
{
    public static event Action<int> OnCoinCollected;
    
    public static void TriggerCoinCollected(int currentCoins)
    {
        OnCoinCollected?.Invoke(currentCoins);
    }
    
    
    
}