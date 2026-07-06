using System;

public static class PlayerObserverManager
{
    public static event Action<int> OnCoinCollected;
    
    public static void NotifyCoinCollected(int currentCoins)
    {
        OnCoinCollected?.Invoke(currentCoins);
    }


   /* public static void NotifyCoinCollected(int coins)
    {
        throw new NotImplementedException();
    }*/
}