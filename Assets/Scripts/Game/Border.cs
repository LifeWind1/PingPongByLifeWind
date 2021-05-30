using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Border : MonoBehaviour
    {
        [SerializeField] private int m_playerIndex;
        
        public UnityEvent<int> OnTriggerEnter;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerEnter?.Invoke(m_playerIndex);
        }
    }
}