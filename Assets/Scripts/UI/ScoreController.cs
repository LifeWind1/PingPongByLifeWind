using Enums;
using Mirror;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreController : NetworkBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_currentScore;
        [SerializeField] private TextMeshProUGUI m_bestScore;
        
        [SerializeField] private TextMeshProUGUI m_playerOneScore;
        [SerializeField] private TextMeshProUGUI m_playerTwoScore;

        [SyncVar(hook = nameof(SetPlayerOneScore))]
        public int m_playerOneCurrentScore;
        
        [SyncVar(hook = nameof(SetPlayerTwoScore))]
        public int m_playerTwoCurrentScore;

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
                m_playerOneCurrentScore = score;
            }
            else
            {
                m_playerTwoCurrentScore = score;
            }
        }

        private void SetPlayerOneScore(int oldScore, int newScore)
        {
            m_playerOneScore.text = newScore.ToString();
        }
        
        private void SetPlayerTwoScore(int oldScore, int newScore)
        {
            m_playerTwoScore.text = newScore.ToString();
        }
    }
}