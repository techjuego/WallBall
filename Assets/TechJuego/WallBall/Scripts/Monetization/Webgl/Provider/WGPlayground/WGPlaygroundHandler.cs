using System;
using System.Collections;
using System.Collections.Generic;
using TechJuego.Framework.Monetization;
using UnityEngine;

namespace TechJuego.Framework
{
    public class WGPlaygroundHandler : MonoBehaviour, IAdGetDetail
    {
        public string GetAdId()
        {
            return string.Empty;
        }

        public void HideBanner()
        {

        }

        public bool IsAddAvailable(AdType adType)
        {
            return true;
        }

        public bool IsShowingAd()
        {
            return false;
        }

        public void ShowBanner(string id)
        {

        }

        public void ShowInstestitial(string id)
        {

        }

        public void ShowRewardAds(string id, Action OnComplete)
        {
#if WGPLAYGROUND
            WeeGooAdManager.Instance.OnSuccess.RemoveAllListeners();
            WeeGooAdManager.Instance.OnSuccess.AddListener(() =>
            {
                OnComplete();
            });
            WeeGooAdManager.Instance.ShowRewardAd();
#endif
        }
    }
}