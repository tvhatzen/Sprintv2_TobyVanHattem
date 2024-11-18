using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the Pause Menu UI
    private bool isPaused = false;  // Whether the game is paused or not

    void Update()
    {
        // Check for the Escape key input to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame(); // Resume if already paused
            else
                PauseGame(); // Pause if not paused
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);  // Hide the pause menu
        Time.timeScale = 1f;           // Resume the game
        isPaused = false;              // Set isPaused to false
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);   // Show the pause menu
        Time.timeScale = 0f;           // Pause the game
        isPaused = true;               // Set isPaused to true
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;  // Make sure to resume time before loading the main menu
        SceneManager.LoadScene("MainMenu"); // Load your main menu scene
    }
}

