using UnityEngine;

namespace UI.Screens
{
    public class ScreenManager : MonoBehaviour
    {
        [SerializeField] private Screen m_mainMenuScreen;
        [SerializeField] private Screen m_settingsScreen;

        private Screen m_currentScreen;
        
        private void Start()
        {
            OpenMenuScreen();
        }

        public void OpenMenuScreen()
        {
            OpenScreen(m_mainMenuScreen);
        }
        
        public void OpenSettingsScreen()
        {
            OpenScreen(m_settingsScreen);
        }
        
        private void OpenScreen(Screen newScreen)
        {
            if (m_currentScreen)
            {
                m_currentScreen.Close();
            }

            newScreen.Open();
            m_currentScreen = newScreen;
        }
    }
}