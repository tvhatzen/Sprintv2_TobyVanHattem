using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Load the main game scene
        SceneManager.LoadScene("Minigame"); // Replace with your game scene name
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
}