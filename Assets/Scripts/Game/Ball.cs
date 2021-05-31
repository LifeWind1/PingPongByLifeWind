using Game.BallColor;
using Mirror;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class Ball : NetworkBehaviour
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

        public bool IsOffline { get; set; }
        
        public Rigidbody2D Rigidbody2D
        {
            get
            {
                if (!m_rigidbody)
                {
                    m_rigidbody = GetComponent<Rigidbody2D>();
                }

                return m_rigidbody;
            }   
        }

        public SpriteRenderer SpriteRenderer
        {
            get
            {
                if (!m_sprite)
                {
                    m_sprite = GetComponent<SpriteRenderer>();
                }

                return m_sprite;
            }   
        }
        
        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
            m_sprite = GetComponent<SpriteRenderer>();
            RandomizeBallParameters();
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            
            Rigidbody2D.simulated = true;
        }
        
        public void ResetBall(Vector2 direction)
        {
            if (isServer || IsOffline)
            {
                Rigidbody2D.Sleep();
                transform.position = Vector2.zero;
                Rigidbody2D.WakeUp();
                Rigidbody2D.AddForce(direction * m_speed);
            }
        }

        public void RandomizeBallParameters()
        {
            m_speed = Random.Range(MinSpeed, MaxSpeed);
            m_size = Random.Range(MinSize, MaxSize);
            
            transform.localScale = new Vector2(m_size, m_size);
        }

        public void SetBallColor(int colorIndex)
        {
            SpriteRenderer.color = m_ballColorArray.BallColors[colorIndex].Color;
        }
    }
}