using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TechJuego.Framework.Sound;
using TechJuego.Framework.Utils;

namespace TechJuego.Framework
{
    public class LevelFailPanel : MonoBehaviour
    {
        [SerializeField] private Button m_NextButton;
        [SerializeField] private RectTransform m_LevelCompletePanel;
        [SerializeField] private TextMeshProUGUI m_BestScore;
        [SerializeField] private TextMeshProUGUI m_CurrentScore;
        private void OnEnable()
        {
            UiUtility.SetButton(m_NextButton, OnClickNextButton);
            SoundEvents.OnPlaySingleShotSound?.Invoke("Win");
            OpenEffect();
            AnalyticsHandler.Instance.PanelLoad(GetType().Name);
            DataHandler.Instance.SetHighScore(GameManager.Instance.m_CurrentScore);
            m_BestScore.text = "Best:-" + DataHandler.Instance.GetHighScore().ToString();
            m_CurrentScore.text ="Score:-" + GameManager.Instance.m_CurrentScore.ToString();
        }
        void OpenEffect()
        {
            m_LevelCompletePanel.localScale = Vector3.zero;
            TechTween.ScaleTo(m_LevelCompletePanel.gameObject, Vector3.one, 0.5f).SetEaseType(EaseTween.EaseOutSine);
        }
        void OnClickNextButton()
        {
            SceneLoader.RestartScene(GameColors.Color1(), 2, ChangeEffect.BottomFill);
        }
    }
}