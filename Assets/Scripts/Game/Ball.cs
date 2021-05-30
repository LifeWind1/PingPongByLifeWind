using Unity.Collections;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        private const float MinSpeed = 150;
        private const float MaxSpeed = 350;

        private const float MinSize = 0.025f;
        private const float MaxSize = 0.1f;
        
        [ReadOnly]
        [SerializeField] private float m_speed;
        
        [ReadOnly]
        [SerializeField] private float m_size;
        
        private Rigidbody2D m_rigidbody;

        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
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
    }
}