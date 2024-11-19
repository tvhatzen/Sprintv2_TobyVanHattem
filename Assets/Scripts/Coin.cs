using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CoinManager coinManager = FindObjectOfType<CoinManager>();
            if (coinManager != null)
            {
                coinManager.CollectCoin(gameObject);
            }
        }
    }
}