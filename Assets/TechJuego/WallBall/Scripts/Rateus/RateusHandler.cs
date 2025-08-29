using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechJuego.Framework.Rateus;
namespace TechJuego.Framework.Rateus
{
    public class RateusHandler : Singleton<RateusHandler>
    {
        protected RateusHandler() { }

        private RateUsData m_RateUsData;
        public void Load()
        {
            m_RateUsData = ResourcesRef.GetRateUsData();
        }

        public void ShowRateus(GameState gameState)
        {

        }
    }
}