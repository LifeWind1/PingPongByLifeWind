using UnityEngine;

namespace Game
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private Player m_playerPrefab;
        [SerializeField] private Transform m_playerOnePlace;
        [SerializeField] private Transform m_playerTwoPlace;
        [SerializeField] private Transform m_playersParent;

        public Player CreatePlayerOne(bool isOffline)
        {
            return CreatePlayer(m_playerOnePlace, isOffline);
        }
        
        public Player CreatePlayerTwo(bool isOffline)
        {
            return CreatePlayer(m_playerTwoPlace, isOffline);
        }

        private Player CreatePlayer(Transform place, bool isOffline)
        {
            Player player = Instantiate(m_playerPrefab, m_playersParent);
            player.transform.position = place.position;
            player.IsOffline = isOffline;

            return player;
        }
    }
}