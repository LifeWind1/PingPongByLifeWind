using UnityEngine;

namespace Game
{
    public static class PlayerPrefsManager
    {
        private const string BestScoreKey = "bestScore";

        public static int GetBestScore()
        {
            return PlayerPrefs.GetInt(BestScoreKey);
        }

        public static void SaveBestScore(int score)
        {
            PlayerPrefs.SetInt(BestScoreKey, score);
            PlayerPrefs.Save();
        }
    }
}