using TechJuego.Framework.Sound;
using TechJuego.Framework.Utils;
using UnityEngine;
using UnityEngine.UI;
namespace TechJuego.Framework
{
    public class HomePanel : MonoBehaviour
    {
        [SerializeField] private Button m_PlayButton;
        private void OnEnable()
        {
            UiUtility.SetButton(m_PlayButton, OnClickPlayButton);
            AnalyticsHandler.Instance.PanelLoad(GetType().Name);
        }
        private void OnClickPlayButton()
        {
            SoundEvents.OnPlaySingleShotSound?.Invoke("Click");
                SceneLoader.LoadScene("Game", GameColors.Color1(), 2, ChangeEffect.BottomFill);
        }
    }
}