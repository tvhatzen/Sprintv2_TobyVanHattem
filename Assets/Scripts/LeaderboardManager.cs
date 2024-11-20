using UnityEngine;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour
{
    private static string leaderboardKey = "Leaderboard";

    public static void SaveScore(int score)
    {
        // Retrieve the leaderboard
        List<int> scores = GetLeaderboard();

        // Add the new score
        scores.Add(score);

        // Sort and keep the top 10 scores
        scores.Sort((a, b) => b.CompareTo(a)); // Sort in descending order
        if (scores.Count > 10)
            scores.RemoveAt(scores.Count - 1);

        // Save the leaderboard
        PlayerPrefs.SetString(leaderboardKey, string.Join(",", scores));
        PlayerPrefs.Save();
    }

    public static List<int> GetLeaderboard()
    {
        // Retrieve the leaderboard from PlayerPrefs
        string savedScores = PlayerPrefs.GetString(leaderboardKey, "");
        List<int> scores = new List<int>();

        if (!string.IsNullOrEmpty(savedScores))
        {
            string[] scoreStrings = savedScores.Split(',');
            foreach (string scoreStr in scoreStrings)
            {
                if (int.TryParse(scoreStr, out int score))
                    scores.Add(score);
            }
        }

        return scores;
    }

    // Add this new method
    public static void ClearLeaderboard()
    {
        PlayerPrefs.DeleteKey(leaderboardKey); // Deletes the leaderboard data
        PlayerPrefs.Save();                   // Ensures changes are written to disk
        Debug.Log("Leaderboard has been cleared.");
    }
}
