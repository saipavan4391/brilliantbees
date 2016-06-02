using UnityEngine;
using System.Collections;

public class DestroyAds {


    public void DestroyBannerAds() {
        AdsControlScript.adsControl.hideBannerAd();
        AdsControlScript.adsControl.DestroyBannerAd();
    }
}
