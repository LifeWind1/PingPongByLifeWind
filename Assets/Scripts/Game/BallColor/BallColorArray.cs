using UnityEngine;

namespace Game.BallColor
{
    [CreateAssetMenu(fileName = "ColorArray", menuName = "Ball/ColorArray", order = 1)]
    public class BallColorArray : ScriptableObject
    {
        [SerializeField] private BallColor[] m_ballColors;

        public BallColor[] BallColors => m_ballColors;
    }
}