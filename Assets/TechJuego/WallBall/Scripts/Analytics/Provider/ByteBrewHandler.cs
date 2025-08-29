using UnityEngine;
#if BYTEBREW
using ByteBrewSDK;
#endif
namespace TechJuego.Framework
{
    public class ByteBrewHandler : MonoBehaviour, IAnatyticsBase
    {
        private void Awake()
        {
#if BYTEBREW
        ByteBrew.InitializeByteBrew();
#endif
        }
        public void LevelFailed(int level)
        {
#if BYTEBREW
        ByteBrew.NewCustomEvent("LevelFailed", level);
#endif
        }
        public void LevelStart(int level)
        {
#if BYTEBREW
        ByteBrew.NewCustomEvent("LevelStart", level);
#endif
        }
        public void RestartLevel(int level)
        {
#if BYTEBREW
        ByteBrew.NewCustomEvent("RestartLevel", level);
#endif
        }
        //endless game
        public void GameOverContinue(int score)
        {
#if BYTEBREW
        ByteBrew.NewCustomEvent("GameOverContinue", score);
#endif
        }
        public void GameOver(int score)
        {
#if BYTEBREW
        ByteBrew.NewCustomEvent("GameOver", score);
#endif
        }
        public void RestartGame()
        {
#if BYTEBREW
        ByteBrew.NewCustomEvent("RestartGame");
#endif
        }
        public void SceneLoad(string scene)
        {
#if BYTEBREW
        ByteBrew.NewCustomEvent(scene);
#endif
        }
        public void LevelComplete(int level)
        {
#if BYTEBREW
        ByteBrew.NewCustomEvent("LevelComplete", level);
#endif
        }
        public void CustomEvent(string value)
        {
#if BYTEBREW
        ByteBrew.NewCustomEvent(value);
#endif
        }
        public void CustomEvent(string key, string value)
        {
#if BYTEBREW
        ByteBrew.NewCustomEvent(key, value);
#endif
        }

        public void Intersitial(string location)
        {
#if BYTEBREW
        ByteBrew.TrackAdEvent(ByteBrewAdTypes.Interstitial, location);
#endif
        }

        public void AdReward(string location)
        {
#if BYTEBREW
        ByteBrew.TrackAdEvent(ByteBrewAdTypes.Reward, location);
#endif
        }

        public void ItemPurchaseWithRealMoney(string item, float value)
        {
#if BYTEBREW
        ByteBrew.NewCustomEvent(item, value);
#endif
        }

        public void ItemPurcahseWithIngameCurrency(string item, string curreny, float value)
        {

        }

        public void GetItemWithReward(string item)
        {

        }
    }
}