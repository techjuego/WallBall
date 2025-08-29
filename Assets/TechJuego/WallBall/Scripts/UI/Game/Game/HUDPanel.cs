using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TechJuego.Framework.Utils;
using TechJuego.Framework.Sound;

namespace TechJuego.Framework
{
    public class HUDPanel : MonoBehaviour
    {
        [SerializeField] private Button m_SettingButton;
        [SerializeField] private TextMeshProUGUI m_Score;
        [SerializeField] private GameObject m_Tutorial;

        private void OnEnable()
        {
            UiUtility.SetButton(m_SettingButton, OnClickSettingButton);
            m_Tutorial.SetActive(false);
            AnalyticsHandler.Instance.PanelLoad(GetType().Name);
            GameEvents.OnStarCollect += GameEvents_OnStarCollect;
        }
        private void OnDisable()
        {
            GameEvents.OnStarCollect -= GameEvents_OnStarCollect;
        }
        private void GameEvents_OnStarCollect()
        {
            m_Score.text = GameManager.Instance.m_CurrentScore.ToString();
        }
        private void OnClickSettingButton()
        {
            InGameUi.Instance.ShowSetingPanel();
            SoundEvents.OnPlaySingleShotSound?.Invoke("Click");
        }
    }
}