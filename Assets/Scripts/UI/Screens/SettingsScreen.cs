using System;
using Game;
using Game.BallColor;
using TMPro;
using UnityEngine;

namespace UI.Screens
{
    public class SettingsScreen : Screen
    {
        [SerializeField] private TMP_Dropdown m_colorDropdown;
        [SerializeField] private BallColorArray m_ballColorArray;
        
        public override void Open()
        {
            base.Open();

            foreach (var colors in m_ballColorArray.BallColors)
            {
                m_colorDropdown.options.Add(new TMP_Dropdown.OptionData(colors.Name));
            }

            m_colorDropdown.value = PlayerPrefsManager.GetBallColor();
        }

        private void OnColorChanged(int colorCode)
        {
            PlayerPrefsManager.SetBallColor(colorCode);
        }

        private void OnEnable()
        {
            m_colorDropdown.onValueChanged.AddListener(OnColorChanged);
        }
        
        private void OnDisable()
        {
            m_colorDropdown.onValueChanged.RemoveListener(OnColorChanged);
        }
    }
}