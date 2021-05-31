using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class NetworkManagerPong : NetworkManager
    {
        [SerializeField] private Transform m_playerOnePlace;
        [SerializeField] private Transform m_playerTwoPlace;
        [SerializeField] private Transform m_playersParent;

        public Player PlayerOne => m_playerOne;
        public Player PlayerTwo => m_playerTwo;
        
        public UnityEvent SecondPlayerConnected;
        
        private Player m_playerOne;
        private Player m_playerTwo;

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            // todo Вынести создание игроков в отдельный класс
            if (numPlayers == 0)
            {
                m_playerOne = Instantiate(playerPrefab, m_playersParent).GetComponent<Player>();
                m_playerOne.transform.position = m_playerOnePlace.position;
                NetworkServer.AddPlayerForConnection(conn, m_playerOne.gameObject);
            }
            else
            {
                m_playerTwo = Instantiate(playerPrefab, m_playersParent).GetComponent<Player>();
                m_playerTwo.transform.position = m_playerTwoPlace.position;
                NetworkServer.AddPlayerForConnection(conn, m_playerTwo.gameObject);
            }

            if (numPlayers == 2)
            {
                SecondPlayerConnected?.Invoke();
            }
        }
    }
}