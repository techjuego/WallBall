using UnityEngine;
using TechJuego.Framework.Sound;
using TechJuego.Framework.Rateus;
using TechJuego.Framework.HapticFeedback;

namespace TechJuego.Framework
{
    public class AutoInitializer
    {
        [RuntimeInitializeOnLoadMethod]
        static void OnRuntimeMethodLoad()
        {
            AdsHandler.Instance.Load();
            SoundManager.Instance.Load();
            RateusHandler.Instance.Load();
            AnalyticsHandler.Instance.Load();
        }
    }
}