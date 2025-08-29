using TechJuego.Framework.Monetization;
using TechJuego.Framework.Rateus;
using UnityEngine;
namespace TechJuego.Framework
{
    public class ResourcesRef
    {
        static MobileAdsData m_MobileAdsData;
        static WebglAdsData m_WebglAdsData;
        static RateUsData m_RateUsData;
        public static RateUsData GetRateUsData()
        {
            if (m_RateUsData == null)
            {
                m_RateUsData = Resources.Load<RateUsData>("Rateus/RateUsData");
            }
            return m_RateUsData;
        }
        public static MobileAdsData GetMobileAdsData()
        {
            if (m_MobileAdsData == null)
            {
                m_MobileAdsData = Resources.Load("Monetization/MobileAdsData") as MobileAdsData;
            }
            return m_MobileAdsData;
        }
        public static WebglAdsData GetWebglAdsData()
        {
            if (m_WebglAdsData == null)
            {
                m_WebglAdsData = Resources.Load("Monetization/WebglAdsData") as WebglAdsData;
            }
            return m_WebglAdsData;
        }
   
    }
}