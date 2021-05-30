using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_currentScore;
        [SerializeField] private TextMeshProUGUI m_bestScore;

        public void SetScore(int currentScore)
        {
            m_currentScore.text = currentScore.ToString();
        }

        public void SetBestScore(int score)
        {
            m_bestScore.text = $"Best: {score}";
        }
    }
}