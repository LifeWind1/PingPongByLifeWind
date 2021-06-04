using Lean.Touch;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Player : NetworkBehaviour
    {
        public UnityEvent OnCollisionEnter;
        
        public bool IsOffline { get; set; }

        private Camera m_mainCamera;
        
        private void Start()
        {
            m_mainCamera = Camera.main;
            LeanTouch.OnFingerUpdate += LeanTouchOnFingerUpdate;
        }

        private void LeanTouchOnFingerUpdate(LeanFinger finger)
        {
            Move(finger.GetWorldPosition(m_mainCamera.transform.position.y, m_mainCamera).x);
        }

        private void Move(float x)
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

        private void OnDisable()
        {
            LeanTouch.OnFingerUpdate -= LeanTouchOnFingerUpdate;
        }
    }
}
