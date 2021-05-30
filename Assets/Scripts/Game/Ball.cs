using Game.BallColor;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class Ball : MonoBehaviour
    {
        private const float MinSpeed = 150;
        private const float MaxSpeed = 350;

        private const float MinSize = 0.025f;
        private const float MaxSize = 0.1f;
        
        [SerializeField] private BallColorArray m_ballColorArray;
        
        private float m_speed;
        private float m_size;
        
        private Rigidbody2D m_rigidbody;
        private SpriteRenderer m_sprite;
        
        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
            m_sprite = GetComponent<SpriteRenderer>();
            RandomizeBallParameters();
        }

        public void ResetBall(Vector2 direction)
        {
            m_rigidbody.Sleep();
            transform.position = Vector2.zero;
            m_rigidbody.WakeUp();
            m_rigidbody.AddForce(direction * m_speed);
        }

        public void RandomizeBallParameters()
        {
            m_speed = Random.Range(MinSpeed, MaxSpeed);
            m_size = Random.Range(MinSize, MaxSize);
            
            transform.localScale = new Vector2(m_size, m_size);
        }

        public void SetBallColor(int colorIndex)
        {
            m_sprite.color = m_ballColorArray.BallColors[colorIndex].Color;
        }
    }
}