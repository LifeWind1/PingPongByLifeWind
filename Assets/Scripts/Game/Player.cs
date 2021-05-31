using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Player : NetworkBehaviour
    {
        public UnityEvent OnCollisionEnter;
        
        public bool IsOffline { get; set; }
        
        public void Move(float x)
        {
            if (isLocalPlayer || IsOffline)
            {
                transform.localPosition = new Vector2(x, transform.localPosition.y);
            }
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            OnCollisionEnter?.Invoke();
        }
    }
}
