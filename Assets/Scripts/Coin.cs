using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CoinManager manager = FindObjectOfType<CoinManager>();
            if (manager != null)
            {
                manager.CollectCoin(gameObject);
            }
        }
    }
}