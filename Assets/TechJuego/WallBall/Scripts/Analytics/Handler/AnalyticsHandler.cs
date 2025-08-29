using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace TechJuego.Framework
{
    public class AnalyticsHandler : Singleton<AnalyticsHandler>
    {
        protected AnalyticsHandler() { }
        private List<IAnatyticsBase> analyticsHandler = new List<IAnatyticsBase>();
        public void Load()
        {
            analyticsHandler.Add(gameObject.AddComponent<UnityHandler>());
            analyticsHandler.Add(gameObject.AddComponent<ByteBrewHandler>());
            analyticsHandler.Add(gameObject.AddComponent<GameArterAnalyticsHandler>());
        }
        // for level based games
        public void LevelComplete(int level)
        {
            foreach (var item in analyticsHandler)
            {
                item.LevelComplete(level);
            }
        }
        public void LevelFailed(int level)
        {
            foreach (var item in analyticsHandler)
            {
                item.LevelFailed(level);
            }
        }
        public void LevelStart(int level)
        {
            foreach (var item in analyticsHandler)
            {
                item.LevelStart(level);
            }
        }
        public void RestartLevel(int level)
        {
            foreach (var item in analyticsHandler)
            {
                item.RestartLevel(level);
            }
        }
        //endless game
        public void GameOverContinue(int score)
        {
            foreach (var item in analyticsHandler)
            {
                item.GameOverContinue(score);
            }
        }
        public void GameOver(int score)
        {
            foreach (var item in analyticsHandler)
            {
                item.GameOver(score);
            }
        }
        public void RestartGame()
        {
            foreach (var item in analyticsHandler)
            {
                item.RestartGame();
            }
        }
        public void SceneLoad()
        {
            foreach (var item in analyticsHandler)
            {
                item.SceneLoad(SceneManager.GetActiveScene().name);
            }
        }
        public void PanelLoad(string name)
        {
            foreach (var item in analyticsHandler)
            {
                item.CustomEvent(name);
            }
        }
        public void Custom(string key)
        {
            foreach (var item in analyticsHandler)
            {
                item.CustomEvent(key);
            }
        }
        public void Custom(string key, string value)
        {
            foreach (var item in analyticsHandler)
            {
                item.CustomEvent(key, value);
            }
        }
    }
}