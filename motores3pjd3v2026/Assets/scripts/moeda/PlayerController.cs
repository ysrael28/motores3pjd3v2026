using UnityEngine;

public class Player : MonoBehaviour
{
    public static int coins = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coins++;

            PlayerObserverManager.NotifyCoinCollected(coins);

            Destroy(other.gameObject);
        }
    }
}