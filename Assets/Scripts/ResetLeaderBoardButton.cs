using UnityEngine;

public class ResetLeaderboardButton : MonoBehaviour
{
    public void ResetLeaderboard()
    {
        // Clear the leaderboard using LeaderboardManager
        LeaderboardManager.ClearLeaderboard();

        // Optional: Update any UI elements that display the leaderboard
        Debug.Log("Leaderboard has been reset.");
    }
}
