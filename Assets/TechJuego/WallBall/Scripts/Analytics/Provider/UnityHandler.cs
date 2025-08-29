using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
namespace TechJuego.Framework
{
    public class UnityHandler : MonoBehaviour, IAnatyticsBase
    {
        public void LevelComplete(int level)
        {
            Analytics.CustomEvent("LevelComplete " + level);
        }
        public void LevelFailed(int level)
        {
            Analytics.CustomEvent("LevelFailed " + level);
        }
        public void LevelStart(int level)
        {
            Analytics.CustomEvent("LevelStart " + level);
        }
        public void RestartLevel(int level)
        {
            Analytics.CustomEvent("RestartLevel " + level);
        }
        //endless game
        public void GameOverContinue(int score)
        {
            Analytics.CustomEvent("GameOverContinue " + score);
        }
        public void GameOver(int score)
        {
            Analytics.CustomEvent("GameOver " + score);
        }
        public void RestartGame()
        {
            Analytics.CustomEvent("RestartGame");
        }
        public void SceneLoad(string scene)
        {
            Analytics.CustomEvent(scene);
        }

        public void CustomEvent(string value)
        {
            Analytics.CustomEvent(value);
        }

        public void CustomEvent(string key, string value)
        {
            Analytics.CustomEvent(key + value);
        }

        public void Intersitial(string location)
        {
            Analytics.CustomEvent("Intersitial" + location);
        }

        public void AdReward(string location)
        {
            Analytics.CustomEvent("AdReward" + location);
        }

        public void ItemPurchaseWithRealMoney(string item, float value)
        {
            Analytics.CustomEvent("ItemName:" + item + "Value" + value);
        }

        public void ItemPurcahseWithIngameCurrency(string item, string curreny, float value)
        {

        }

        public void GetItemWithReward(string item)
        {

        }
    }
}