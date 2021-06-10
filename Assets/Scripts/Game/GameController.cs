using Enums;
using Mirror;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private ScoreController m_scoreController;
        [SerializeField] private PlayerManager m_playerManager;
        [SerializeField] private Ball m_ball;
        [SerializeField] private Border m_borderOne;
        [SerializeField] private Border m_borderTwo;
        [SerializeField] private NetworkManagerPong m_networkManager;

        private Player m_playerOne;
        private Player m_playerTwo;

        private int m_bestScore;

        private int m_currentScoreOne = 0;
        private int m_currentScoreTwo = 0;

        private GameMode m_currentGameMode;

        private void Start()
        {
            LoadSettings();

            m_borderOne.OnTriggerEnter.AddListener(OnLoose);
            m_borderTwo.OnTriggerEnter.AddListener(OnLoose);

            m_currentScoreOne = 0;
            m_currentScoreTwo = 0;

            if (m_currentGameMode == GameMode.Offline)
            {
                m_networkManager.gameObject.SetActive(false);
                m_ball.gameObject.SetActive(true);
                m_scoreController.gameObject.SetActive(true);
                
                m_playerOne = m_playerManager.CreatePlayerOne(true);
                m_playerTwo = m_playerManager.CreatePlayerTwo(true);

                m_playerOne.OnCollisionEnter.AddListener(OnBallHit);
                m_playerTwo.OnCollisionEnter.AddListener(OnBallHit);
                
                StartGame();
            }
            else
            {
                m_networkManager.gameObject.SetActive(true);
                m_networkManager.SecondPlayerConnected.AddListener(StartGame);
            }
        }
        
        private void StartGame()
        {
            if (m_currentGameMode == GameMode.Online)
            {
                m_ball.IsOffline = false;
                NetworkServer.Spawn(m_ball.gameObject);
                m_playerOne = m_networkManager.PlayerOne;
                m_playerTwo = m_networkManager.PlayerTwo;
            }
            else
            {
                m_ball.IsOffline = true;
            }
            
            ResetBall();
        }

        private void LoadSettings()
        {
            m_currentGameMode = PlayerPrefsManager.GetGameMode();
            m_bestScore = PlayerPrefsManager.GetBestScore();

            m_scoreController.SetGameMode(m_currentGameMode);

            if (m_currentGameMode == GameMode.Offline)
            {
                m_scoreController.SetBestScore(m_bestScore);
                m_ball.Rigidbody2D.simulated = true;
            }

            m_ball.SetBallColor(PlayerPrefsManager.GetBallColor());
        }

        private void OnBallHit()
        {
            if (m_currentGameMode == GameMode.Online)
            {
                return;
            }
            
            m_currentScoreOne++;
            m_scoreController.SetScore(m_currentScoreOne);

            if (m_currentScoreOne > m_bestScore)
            {
                m_bestScore = m_currentScoreOne;
                m_scoreController.SetBestScore(m_bestScore);
            }
        }
        
        private void OnLoose(int playerIndex)
        {
            if (m_currentGameMode == GameMode.Offline)
            {
                m_currentScoreOne = 0;
                m_scoreController.SetScore(m_currentScoreOne);

                PlayerPrefsManager.SaveBestScore(m_bestScore);
                
                ResetBall();
            }
            else
            {
                if (playerIndex == 0)
                {
                    m_currentScoreOne++;
                    m_scoreController.SetOnlineScore(0, m_currentScoreOne);
                }
                else
                {
                    m_currentScoreTwo++;
                    m_scoreController.SetOnlineScore(1, m_currentScoreTwo);
                }

                ResetBall();
            }
        }

        private void ResetBall()
        {
            Vector2 direction = new Vector2(1,Random.Range(1.5f, -1.5f));
            
            if(Random.Range(0,2) == 1)
            {
                direction.x *= -1;
            }

            m_ball.RandomizeBallParameters();
            m_ball.ResetBall(direction);
        }

        private void OnDisable()
        {
            PlayerPrefsManager.SaveBestScore(m_bestScore);
            
            m_borderOne.OnTriggerEnter.RemoveListener(OnLoose);
            m_borderTwo.OnTriggerEnter.RemoveListener(OnLoose);
            
            if (m_playerOne)
            {
                m_playerOne.OnCollisionEnter.RemoveListener(OnBallHit);
            }

            if (m_playerTwo)
            {
                m_playerTwo.OnCollisionEnter.RemoveListener(OnBallHit);
            }
        }
    }
}