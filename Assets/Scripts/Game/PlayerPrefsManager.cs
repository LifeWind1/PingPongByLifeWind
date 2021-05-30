using UnityEngine;

namespace Game
{
    public static class PlayerPrefsManager
    {
        private const string BestScoreKey = "bestScore";
        private const string BallColorKey = "ballColor";

        public static int GetBestScore()
        {
            return PlayerPrefs.GetInt(BestScoreKey);
        }

        public static void SaveBestScore(int score)
        {
            PlayerPrefs.SetInt(BestScoreKey, score);
            PlayerPrefs.Save();
        }

        public static int GetBallColor()
        {
            return PlayerPrefs.GetInt(BallColorKey);
        }
        
        public static void SetBallColor(int colorIndex)
        { 
            PlayerPrefs.SetInt(BallColorKey, colorIndex);
            PlayerPrefs.Save();
        }
    }
}