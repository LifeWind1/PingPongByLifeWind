using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_currentScore;

        public void SetScore(int currentScore)
        {
            m_currentScore.text = currentScore.ToString();
        }
    }
}