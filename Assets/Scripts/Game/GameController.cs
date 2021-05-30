using Lean.Touch;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private ScoreController m_scoreController;
        [SerializeField] private Player m_playerOne;
        [SerializeField] private Player m_playerTwo;
        [SerializeField] private Ball m_ball;
        [SerializeField] private Border m_borderOne;
        [SerializeField] private Border m_borderTwo;

        private Camera m_mainCamera;

        private int m_bestScore;
        
        private int m_currentScoreOne;
        
        private void Start()
        {
            m_mainCamera = Camera.main;
            LeanTouch.OnFingerUpdate += LeanTouchOnFingerUpdate;
            m_borderOne.OnTriggerEnter.AddListener(OnLoose);
            m_borderTwo.OnTriggerEnter.AddListener(OnLoose);
            m_playerOne.OnCollisionEnter.AddListener(OnBallHit);
            m_playerTwo.OnCollisionEnter.AddListener(OnBallHit);

            m_bestScore = PlayerPrefsManager.GetBestScore();
            m_scoreController.SetBestScore(m_bestScore);
            
            ResetBall();
        }

        private void LeanTouchOnFingerUpdate(LeanFinger finger)
        {
            float positionX = finger.GetWorldPosition(m_mainCamera.transform.position.y, m_mainCamera).x;
            
            m_playerOne.Move(positionX);
            m_playerTwo.Move(positionX);
        }
        
        private void OnBallHit()
        {
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
            m_currentScoreOne = 0;
            m_scoreController.SetScore(m_currentScoreOne);
            
            PlayerPrefsManager.SaveBestScore(m_bestScore);
            
            ResetBall();
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