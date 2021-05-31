using Enums;
using Game;
using Game.BallColor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class SettingsScreen : Screen
    {
        [SerializeField] private TMP_Dropdown m_colorDropdown;
        [SerializeField] private Toggle m_onlineModeToggle;
        [SerializeField] private BallColorArray m_ballColorArray;
        
        public override void Open()
        {
            base.Open();

            foreach (var colors in m_ballColorArray.BallColors)
            {
                m_colorDropdown.options.Add(new TMP_Dropdown.OptionData(colors.Name));
            }

            m_colorDropdown.value = PlayerPrefsManager.GetBallColor();

            m_onlineModeToggle.isOn = PlayerPrefsManager.GetGameMode() == GameMode.Online;
        }

        private void OnColorChanged(int colorCode)
        {
            PlayerPrefsManager.SetBallColor(colorCode);
        }

        private void OnGameModeChanged(bool isOnline)
        {
            PlayerPrefsManager.SetGameMode(isOnline ? GameMode.Online : GameMode.Offline);
        }
        
        private void OnEnable()
        {
            m_colorDropdown.onValueChanged.AddListener(OnColorChanged);
            m_onlineModeToggle.onValueChanged.AddListener(OnGameModeChanged);
        }
        
        private void OnDisable()
        {
            m_colorDropdown.onValueChanged.RemoveListener(OnColorChanged);
            m_onlineModeToggle.onValueChanged.RemoveListener(OnGameModeChanged);
        }
    }
}