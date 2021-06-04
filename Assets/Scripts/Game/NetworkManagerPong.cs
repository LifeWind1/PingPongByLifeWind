using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class NetworkManagerPong : NetworkManager
    {
        [SerializeField] private PlayerManager m_playerManager;

        public Player PlayerOne => m_playerOne;
        public Player PlayerTwo => m_playerTwo;
        
        public UnityEvent SecondPlayerConnected;

        private Player m_playerOne;
        private Player m_playerTwo;

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            if (numPlayers == 0)
            {
                m_playerOne = m_playerManager.CreatePlayerOne(false);
                NetworkServer.AddPlayerForConnection(conn, m_playerOne.gameObject);
            }
            else
            {
                m_playerTwo = m_playerManager.CreatePlayerTwo(false);
                NetworkServer.AddPlayerForConnection(conn, m_playerTwo.gameObject);
            }

            if (numPlayers == 2)
            {
                SecondPlayerConnected?.Invoke();
            }
        }
    }
}