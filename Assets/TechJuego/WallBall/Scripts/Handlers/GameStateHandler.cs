using TechJuego.Framework.Rateus;
using TechJuego.Framework.Utils;

namespace TechJuego.Framework
{
    public class GameStateHandler : Singleton<GameStateHandler>
    {
        protected GameStateHandler() { }

        private GameState GameState;
        public GameState m_GameState
        {
            get { return GameState; }
            set
            {
                GameState = value;
                switch (value)
                {
                    case GameState.None:
                        break;
                    case GameState.Gameplay:
                        break;
                    case GameState.InGameSetting:
                        break;
                    case GameState.LevelFailed:
                        TechTween.DelayCall(gameObject, 1, () =>
                        {
                            GameEvents.LevelFailed?.Invoke();
                        });
                        break;
                }
                AdsHandler.Instance.ShowAds(value);
#if UNITY_ANDROID || UNITY_IPHONE
                RateusHandler.Instance.ShowRateus(value);
#endif
            }
        }
    }
}