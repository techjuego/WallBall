namespace TechJuego.Framework
{
    public interface IAnatyticsBase
    {
        //common
        void SceneLoad(string name);
        //levelbased
        void LevelComplete(int level);
        void LevelFailed(int level);
        void LevelStart(int level);
        void RestartLevel(int level);
        //endless game
        void GameOverContinue(int score);
        void GameOver(int score);
        void RestartGame();
        void CustomEvent(string value);
        void CustomEvent(string key, string value);
        void Intersitial(string location);
        void AdReward(string location);
        void ItemPurchaseWithRealMoney(string item, float value);
        void ItemPurcahseWithIngameCurrency(string item, string curreny, float value);
        void GetItemWithReward(string item);
    }
}