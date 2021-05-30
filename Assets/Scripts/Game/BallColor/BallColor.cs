using UnityEngine;

namespace Game.BallColor
{
    [CreateAssetMenu(fileName = "Color", menuName = "Ball/Color", order = 0)]
    public class BallColor : ScriptableObject
    {
        [SerializeField] private string m_colorName;
        [SerializeField] private Color m_color;

        public string Name => m_colorName;

        public Color Color => m_color;
    }
}