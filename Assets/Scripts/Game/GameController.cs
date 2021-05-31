using Enums;
using Lean.Touch;
using Mirror;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private ScoreController m_scoreController;
        [SerializeField] private Player m_playerPrefab;
        [SerializeField] private Transform m_playerOnePlace;
        [SerializeField] private Transform m_playerTwoPlace;
        [SerializeField] private Transform m_playersParent;
        [SerializeField] private Ball m_ball;
        [SerializeField] private Border m_borderOne;
        [SerializeField] private Border m_borderTwo;
        [SerializeField] private NetworkManagerPong m_networkManager;

        private Player m_playerOne;
        private Player m_playerTwo;
        
        private Camera m_mainCamera;

        private int m_bestScore;
        
        private int m_currentScoreOne = 0;
        private int m_currentScoreTwo = 0;

        private GameMode m_currentGameMode;

        private void Awake()
        {
            m_mainCamera = Camera.main;
        }

        private void Start()
        {
            LoadSettings();

            if (m_currentGameMode == GameMode.Offline)
            {
                m_networkManager.gameObject.SetActive(false);
                m_ball.gameObject.SetActive(true);

                // todo Вынести создание игроков в отдельный класс
                m_playerOne = Instantiate(m_playerPrefab, m_playersParent);
                m_playerOne.transform.position = m_playerOnePlace.position;
                m_playerOne.IsOffline = true;
                
                m_playerTwo = Instantiate(m_playerPrefab, m_playersParent);
                m_playerTwo.transform.position = m_playerTwoPlace.position;
                m_playerTwo.IsOffline = true;
                
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
                m_playerOne.IsOffline = false;
                m_playerTwo.IsOffline = false;
            }
            else
            {
                m_ball.IsOffline = true;
            }
            
            LeanTouch.OnFingerUpdate += LeanTouchOnFingerUpdate;
            m_borderOne.OnTriggerEnter.AddListener(OnLoose);
            m_borderTwo.OnTriggerEnter.AddListener(OnLoose);
            m_playerOne.OnCollisionEnter.AddListener(OnBallHit);
            m_playerTwo.OnCollisionEnter.AddListener(OnBallHit);

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
        
        private void LeanTouchOnFingerUpdate(LeanFinger finger)
        {
            float positionX = finger.GetWorldPosition(m_mainCamera.transform.position.y, m_mainCamera).x;
            
            m_playerOne.Move(positionX);
            m_playerTwo.Move(positionX);
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
                    m_scoreController.SetOnlineScore(playerIndex, m_currentScoreOne);
                }
                else
                {
                    m_currentScoreTwo++;
                    m_scoreController.SetOnlineScore(playerIndex, m_currentScoreTwo);
                }

                if (m_ball.isServer)
                {
                    ResetBall();
                }
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
            
            LeanTouch.OnFingerUpdate -= LeanTouchOnFingerUpdate;
            m_borderOne.OnTriggerEnter.RemoveListener(OnLoose);
            m_borderTwo.OnTriggerEnter.RemoveListener(OnLoose);
            m_playerOne.OnCollisionEnter.RemoveListener(OnBallHit);
            m_playerTwo.OnCollisionEnter.RemoveListener(OnBallHit);
        }
    }
}