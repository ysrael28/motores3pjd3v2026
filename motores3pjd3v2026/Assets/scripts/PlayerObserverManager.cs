using System;

public static class PlayerObserverManager
{
    // --- Eventos e Métodos em Português ---
    public static event Action<int> OnMoedaCollected;

    public static void NotifyMoedaCollected(int valor)
    {
        OnMoedaCollected?.Invoke(valor);
        OnCoinCollected?.Invoke(valor); // Dispara ambos para garantir sincronia
    }

    // --- Eventos e Métodos em Inglês (Exigidos pelos novos erros) ---
    public static event Action<int> OnCoinCollected;

    public static void NotifyCoinCollected(int valor)
    {
        OnCoinCollected?.Invoke(valor);
        OnMoedaCollected?.Invoke(valor); // Dispara ambos para garantir sincronia
    }
}