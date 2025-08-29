using System;
namespace TechJuego.Framework.Monetization
{
    [Serializable]
    public class AdEvents
    {
        public GameState gameEvent;
        public AdType AddToCall;
        public int everyLevel;
        public int calls;
    }
}