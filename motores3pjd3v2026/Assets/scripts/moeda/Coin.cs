using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
       
            PlayerController player = other.GetComponent<PlayerController>();
            
           
            if (player != null)
            {
                player.AddCoin();
            }
            else
            {
                Debug.LogWarning("<color=red>[Coin]</color> Colidi com o Player, mas não achei o script de controle nele! Verifique o nome do script.");
            }

           
            Destroy(gameObject);
        }
    }
}