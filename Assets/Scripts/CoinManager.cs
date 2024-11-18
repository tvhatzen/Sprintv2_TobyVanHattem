using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public GameObject coinPrefab; // Assign the Coin prefab in the Inspector
    public Text scoreText; // Assign the Canvas Text UI in the Inspector
    public int maxCoins = 30; // Maximum number of coins allowed in the world
    public float coinLifetime = 30f; // Time before a coin despawns

    private int score = 0; // Keeps track of the player's score
    private List<GameObject> coins = new List<GameObject>(); // List to keep track of active coins

    void Start()
    {
        for (int i = 0; i < maxCoins; i++)
        {
            SpawnCoin();
        }
    }

    void SpawnCoin()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-10f, 10f), // Random X position
            Random.Range(1f, 3f),   // Random Y position (slightly above ground level)
            Random.Range(-10f, 10f) // Random Z position
        );

        GameObject coin = Instantiate(coinPrefab, randomPosition, Quaternion.identity);
        coins.Add(coin);

        // Set a despawn timer for the coin
        StartCoroutine(DespawnCoin(coin));
    }

    IEnumerator DespawnCoin(GameObject coin)
    {
        yield return new WaitForSeconds(coinLifetime);

        if (coin != null)
        {
            coins.Remove(coin);
            Destroy(coin);

            // Spawn a new coin to maintain the limit
            SpawnCoin();
        }
    }

    public void CollectCoin(GameObject coin)
    {
        coins.Remove(coin);
        Destroy(coin);

        // Increment the score
        score += 10; // Add your desired score value here
        scoreText.text = "Score: " + score;

        // Spawn a new coin to maintain the limit
        SpawnCoin();
    }
}