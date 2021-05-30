using UnityEngine;

namespace Game
{
    public class BorderPositionAndSizeChanger : MonoBehaviour
    {
        [SerializeField] private Transform m_topBorder;
        [SerializeField] private Transform m_bottomBorder;
        [SerializeField] private Transform m_leftBorder;
        [SerializeField] private Transform m_rightBorder;

        private Camera m_mainCamera;
        
        private BoxCollider2D m_topCollider;
        private BoxCollider2D m_bottomCollider;
        private BoxCollider2D m_leftCollider;
        private BoxCollider2D m_rightCollider;

        private void Awake()
        {
            m_mainCamera = Camera.main;
            m_topCollider = m_bottomBorder.GetComponent<BoxCollider2D>();
            m_bottomCollider = m_topBorder.GetComponent<BoxCollider2D>();
            m_leftCollider = m_leftBorder.GetComponent<BoxCollider2D>();
            m_rightCollider = m_rightBorder.GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
            SetPositions();
            SetColliderSizes();
        }

        private void SetPositions()
        {
            m_topBorder.position = m_mainCamera.ScreenToWorldPoint(new Vector2(Screen.width / 2f, Screen.height));
            m_bottomBorder.position = m_mainCamera.ScreenToWorldPoint(new Vector2(Screen.width / 2f, 0f));
            m_leftBorder.position = m_mainCamera.ScreenToWorldPoint(new Vector2(0f, Screen.height / 2f));
            m_rightBorder.position = m_mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height / 2f));
        }

        private void SetColliderSizes()
        {
            Vector2 vertical = new Vector2 (0.1f, m_mainCamera.orthographicSize * 2);
            Vector2 horizontal = new Vector2 (vertical.y * Screen.width / Screen.height, 0.1f);

            m_topCollider.size = horizontal;
            m_bottomCollider.size = horizontal;
            m_leftCollider.size = vertical;
            m_rightCollider.size = vertical;
        }
    }
}