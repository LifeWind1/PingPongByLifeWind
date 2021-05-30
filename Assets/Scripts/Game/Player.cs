using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int m_index;

        public UnityEvent OnCollisionEnter;
        
        public void Move(float x)
        {
            transform.localPosition = new Vector2(x,  transform.localPosition.y);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnCollisionEnter?.Invoke();
        }
    }
}
