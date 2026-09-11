using System;

public static class PlayerObserverManager
{
    public static event Action<int> OnMoedaCollected;
    public static event Action<int> OnCoinCollected;

    // Dispara ambos os eventos para manter sincronia entre chamadas em PT e EN
    public static void NotifyMoedaCollected(int valor)
    {
        OnMoedaCollected?.Invoke(valor);
        OnCoinCollected?.Invoke(valor);
    }

    public static void NotifyCoinCollected(int valor)
    {
        OnCoinCollected?.Invoke(valor);
        OnMoedaCollected?.Invoke(valor);
    }
}