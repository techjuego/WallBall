using UnityEngine;
using UnityEngine.UI;
using TechJuego.Framework.Utils;
using TechJuego.Framework.Sound;

namespace TechJuego.Framework
{
    public class PanelBase : MonoBehaviour
    {
        public Button m_SettingButton;
        private void OnEnable()
        {
            transform.SetAsLastSibling();
            UiUtility.SetButton(m_SettingButton, OnClickSettingButton);
            AnalyticsHandler.Instance.PanelLoad(GetType().Name);
        }
        private void OnClickSettingButton()
        {
            SoundEvents.OnPlaySingleShotSound?.Invoke("Click");
            MainMenuPanel.Instance.m_SettingPanel.gameObject.SetActive(true);
        }
    }
}