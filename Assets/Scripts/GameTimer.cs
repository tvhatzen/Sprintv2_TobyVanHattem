using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float gameTime = 60f;              // Total game time in seconds
    public TMP_Text timerText;               // Timer UI on the ScoreUI panel
    public TMP_Text scoreText;               // Score UI on the ScoreUI panel
    public GameObject gameOverMenu;          // GameOver panel
    public TMP_Text finalScoreText;          // Final score display on GameOver panel
    public TMP_Text leaderboardText;         // Leaderboard display on GameOver panel

    private bool gameEnded = false;          // Tracks if the game has ended
    private int currentScore = 0;            // Player's score

    void Start()
    {
        // Initialize UI elements
        UpdateScoreUI();
        UpdateTimerUI();
    }

    void Update()
    {
        if (gameEnded) return;

        // Update the timer countdown
        gameTime -= Time.deltaTime;
        if (gameTime <= 0)
        {
            gameTime = 0;
            EndGame();
        }

        // Update the timer text in real time
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        timerText.text = $"Time: {Mathf.CeilToInt(gameTime)}";
    }

    void UpdateScoreUI()
    {
        scoreText.text = $"Score: {currentScore}";
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreUI();
    }

    void EndGame()
    {
        gameEnded = true;
        Time.timeScale = 0f; // Pause the game
        gameOverMenu.SetActive(true);

        // Show the final score in the GameOver menu
        finalScoreText.text = $"Your Score: {currentScore}";

        // Save the score to the leaderboard
        LeaderboardManager.SaveScore(currentScore);

        // Display the leaderboard
        ShowLeaderboard();
    }

    void ShowLeaderboard()
    {
        // Retrieve and display leaderboard scores
        var scores = LeaderboardManager.GetLeaderboard();
        leaderboardText.text = "Leaderboard:\n";
        for (int i = 0; i < scores.Count; i++)
        {
            leaderboardText.text += $"{i + 1}. {scores[i]}\n";
        }
    }

    public void RetryGame()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene("MainMenu");
    }
}
