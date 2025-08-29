using UnityEngine;
using UnityEngine.UI;
using TechJuego.Framework.Sound;
using TechJuego.Framework.Utils;
using TechJuego.Framework.HapticFeedback;

namespace TechJuego.Framework
{
    public class InGameSettingsPanel : MonoBehaviour
    {
        [SerializeField] private Button m_BackButton;
        [SerializeField] private SwitchButton m_MusicButton;
        [SerializeField] private SwitchButton m_SFXButton;
        [SerializeField] private SwitchButton m_VibrateButton;
        [SerializeField] private Button m_PrivacyPolicyButton;
        [SerializeField] private Button m_HomeButton;
        [SerializeField] private RectTransform m_SettingPanel;
        private void OnEnable()
        {
            GameStateHandler.Instance.m_GameState = GameState.InGameSetting;
            transform.SetAsLastSibling();
            UiUtility.SetButton(m_HomeButton, OnClickHomeButton);
            UiUtility.SetButton(m_BackButton, CloseSettingPannel);
            UiUtility.SetButton(m_PrivacyPolicyButton, OpenPolicy);

            m_MusicButton.Initialize(SoundSetting.MusicVariable);
            m_MusicButton.OnClicEvent.RemoveAllListeners();
            m_MusicButton.OnClicEvent.AddListener(Button_Music);

            m_SFXButton.Initialize(SoundSetting.SfxVariable);
            m_SFXButton.OnClicEvent.RemoveAllListeners();
            m_SFXButton.OnClicEvent.AddListener(Button_SFX);

            m_VibrateButton.Initialize(HapticSetting.HapticViration);
            OpenEffect();
            AnalyticsHandler.Instance.PanelLoad(GetType().Name);
        }

        // Handler for the sound toggle button click
        void Button_Music(bool value)
        {
            if (value)
            {
                SoundEvents.OnMusicSettingUpdate?.Invoke();
            }
            else
            {
                SoundEvents.OnMusicSettingUpdate?.Invoke();
            }
            // Adjust the AudioListener volume based on the sound setting
        }
        void Button_SFX(bool value)
        {
            // Adjust the AudioListener volume based on the sound setting
        }
        
        void OnClickHomeButton()
        {
            SoundEvents.OnPlaySingleShotSound?.Invoke("Click");
            SceneLoader.LoadScene("Menu", GameColors.Color1(), 1.5f, ChangeEffect.BottomFill);
        }
        public void CloseSettingPannel()
        {
            CloseEffect();
            SoundEvents.OnPlaySingleShotSound?.Invoke("Click");
        }
        void OpenPolicy()
        {
            Application.OpenURL("https://techjuego.com/privacypolicy/");
            SoundEvents.OnPlaySingleShotSound?.Invoke("Click");
        }
        void OpenEffect()
        {
            TechTween.SetPosition(m_SettingPanel, Vector3.down * 1000);
            m_SettingPanel.localScale = Vector3.zero;
            TechTween.ScaleTo(m_SettingPanel.gameObject, Vector3.one, 0.5f).SetEaseType(EaseTween.EaseOutSine);
            TechTween.MoveTo(m_SettingPanel, Vector3.zero, 0.5f, true).SetEaseType(EaseTween.EaseOutBack);
        }
        void CloseEffect()
        {
            TechTween.ScaleTo(m_SettingPanel.gameObject, Vector3.zero, 0.5f).SetEaseType(EaseTween.EaseInSine);
            TechTween.MoveTo(m_SettingPanel, Vector3.down * 2000, 0.5f, true)
                .SetEaseType(EaseTween.EaseInSine)
                .GetCompleteCallback(() =>
                {
                    gameObject.SetActive(false);
                });
        }
    }
}