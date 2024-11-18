using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public GameObject coinPrefab; // Assign the Coin prefab in the Inspector
    public TextMeshProUGUI scoreText; // Assign the TextMeshProUGUI element in the Inspector
    public int maxCoins = 30; // Maximum number of coins allowed in the world
    public float coinLifetime = 30f; // Time before a coin despawns
    public LayerMask whatisGround; // Assign the layer of ground in the Inspector
    public Vector3 spawnAreaSize = new Vector3(20f, 0f, 20f); // Define the area for spawning coins
    public float spawnInterval = 2f; // Time interval between coin spawns

    private int score = 0; // Keeps track of the player's score
    private List<GameObject> coins = new List<GameObject>(); // List to keep track of active coins

    void Start()
    {
        // Start the spawn coroutine
        StartCoroutine(SpawnCoinsOverTime());
    }

    IEnumerator SpawnCoinsOverTime()
    {
        while (true)
        {
            if (coins.Count < maxCoins)
            {
                SpawnCoin();
            }

            // Wait for the spawn interval before spawning the next coin
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnCoin()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2), // Random X within spawn area
            10f, // Starting Y position (above ground)
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)  // Random Z within spawn area
        );

        // Use a raycast to find the ground position
        if (Physics.Raycast(randomPosition, Vector3.down, out RaycastHit hit, Mathf.Infinity, whatisGround))
        {
            Vector3 groundPosition = hit.point; // The position on the ground
            GameObject coin = Instantiate(coinPrefab, groundPosition, Quaternion.identity);
            coins.Add(coin);

            // Set a despawn timer for the coin
            StartCoroutine(DespawnCoin(coin));
        }
    }

    IEnumerator DespawnCoin(GameObject coin)
    {
        yield return new WaitForSeconds(coinLifetime);

        if (coin != null)
        {
            coins.Remove(coin);
            Destroy(coin);
        }
    }

    public void CollectCoin(GameObject coin)
    {
        coins.Remove(coin);
        Destroy(coin);

        // Increment the score
        score += 10; // Add your desired score value here
        scoreText.text = "Score: " + score;
    }
}
