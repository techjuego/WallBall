using System;
using TechJuego.Framework.Rateus;
using TechJuego.Framework.Utils;
using UnityEngine;

namespace TechJuego.Framework
{
    public class InGameUi : MonoBehaviour
    {
        public static InGameUi Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InGameUi();
                }
                return _instance;
            }
        }
        private static InGameUi _instance;
        public InGameUi()
        {
            _instance = this;
        }
        [SerializeField] private HUDPanel m_HUDUI;
        [SerializeField] private LevelFailPanel m_LevelFailPanel;
        [SerializeField] private RateUsPopup m_RateUsPopup;
        [SerializeField] private InGameSettingsPanel m_InGameSettingsPanel;
        private void Awake()
        {
            m_LevelFailPanel.gameObject.SetActive(false);
            m_InGameSettingsPanel.gameObject.SetActive(false);
            m_RateUsPopup.gameObject.SetActive(false);
            m_HUDUI.gameObject.SetActive(true);
        }
        private void OnEnable()
        {
            GameEvents.OnShowRateUs += GameEvents_OnShowRateUs;
            GameEvents.LevelFailed += GameEvents_LevelFailed;
            AnalyticsHandler.Instance.SceneLoad();
            AnalyticsHandler.Instance.PanelLoad(GetType().Name);
        }

        private void OnDisable()
        {
            GameEvents.LevelFailed -= GameEvents_LevelFailed;
            GameEvents.OnShowRateUs -= GameEvents_OnShowRateUs;
        }
        private void GameEvents_LevelFailed()
        {
            m_LevelFailPanel.gameObject.SetActive(true);
        }
        public void ShowSetingPanel()
        {
            m_InGameSettingsPanel.gameObject.SetActive(true);
        }
        public void ShowHintPanel()
        {
        }
        private void GameEvents_OnShowRateUs()
        {
            m_RateUsPopup.gameObject.SetActive(true);
        }
    }
}
