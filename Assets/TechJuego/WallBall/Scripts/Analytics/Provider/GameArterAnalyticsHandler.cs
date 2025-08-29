using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TechJuego.Framework
{
    public class GameArterAnalyticsHandler : MonoBehaviour, IAnatyticsBase
    {
        public void AdReward(string location)
        {
#if GAMEARTER
            Garter.I.AnalyticsEvent("AdReward", location);
#endif
        }

        public void CustomEvent(string value)
        {
#if GAMEARTER
            Garter.I.AnalyticsEvent(value);
#endif
        }

        public void CustomEvent(string key, string value)
        {
#if GAMEARTER
            Garter.I.AnalyticsEvent(value, value);
#endif
        }

        public void GameOver(int score)
        {
#if GAMEARTER
            Garter.I.AnalyticsEvent("GameOver", score.ToString());
#endif
        }

        public void GameOverContinue(int score)
        {
#if GAMEARTER
            Garter.I.AnalyticsEvent("GameOverContinue", score.ToString());
            Garter.I.Event("GameOverContinue", score);
#endif
        }

        public void GetItemWithReward(string item)
        {
#if GAMEARTER
            Garter.I.AnalyticsEvent("GetItemWithReward", item);
#endif
        }

        public void Intersitial(string location)
        {
#if GAMEARTER
            Garter.I.AnalyticsEvent("Intersitial", location);
#endif
        }

        public void ItemPurcahseWithIngameCurrency(string item, string curreny, float value)
        {
#if GAMEARTER
            Garter.I.AnalyticsEvent("ItemPurchaseWithRealMoney", item, curreny.ToString());
#endif
        }

        public void ItemPurchaseWithRealMoney(string item, float value)
        {
#if GAMEARTER
            Garter.I.AnalyticsEvent("ItemPurchaseWithRealMoney", item, value.ToString());
#endif
        }

        public void LevelComplete(int level)
        {
#if GAMEARTER
            Garter.I.AnalyticsEvent("LevelComplete", level.ToString());
            Garter.I.Event("LevelComplete", level);
#endif
        }

        public void LevelFailed(int level)
        {
#if GAMEARTER
            Garter.I.AnalyticsEvent("LevelFailed", level.ToString());
            Garter.I.Event("LevelFailed", level);
#endif
        }

        public void LevelStart(int level)
        {
#if GAMEARTER
            Garter.I.AnalyticsEvent("LevelStart", level.ToString());
            Garter.I.Event("LevelStart", level);
#endif
        }

        public void RestartGame()
        {
#if GAMEARTER
            Garter.I.AnalyticsEvent("RestartGame");
#endif
        }

        public void RestartLevel(int level)
        {
#if GAMEARTER
            Garter.I.AnalyticsEvent("RestartLevel", level.ToString());
            Garter.I.Event("RestartLevel", level);
#endif
        }

        public void SceneLoad(string name)
        {
#if GAMEARTER
            Garter.I.AnalyticsEvent("SceneLoad", name);
#endif
        }
    }
}