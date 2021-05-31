using Enums;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_currentScore;
        [SerializeField] private TextMeshProUGUI m_bestScore;

        [SerializeField] private TextMeshProUGUI m_playerOneScore;
        [SerializeField] private TextMeshProUGUI m_playerTwoScore;

        public void SetGameMode(GameMode gameMode)
        {
            if (gameMode == GameMode.Offline)
            {
                m_currentScore.gameObject.SetActive(true);
                m_bestScore.gameObject.SetActive(true);
                
                m_playerOneScore.gameObject.SetActive(false);
                m_playerTwoScore.gameObject.SetActive(false);
            }
            else
            {
                m_currentScore.gameObject.SetActive(false);
                m_bestScore.gameObject.SetActive(false);
                
                m_playerOneScore.gameObject.SetActive(true);
                m_playerTwoScore.gameObject.SetActive(true);
            }
        }
        
        public void SetScore(int currentScore)
        {
            m_currentScore.text = currentScore.ToString();
        }

        public void SetBestScore(int score)
        {
            m_bestScore.text = $"Best: {score}";
        }

        public void SetOnlineScore(int index, int score)
        {
            if (index == 0)
            {
                m_playerOneScore.text = score.ToString();
            }
            else
            {
                m_playerTwoScore.text = score.ToString();
            }
        }
    }
}