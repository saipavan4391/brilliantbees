using UnityEngine;
using System.Collections;
using GoogleMobileAds; 
using GoogleMobileAds.Api;
using System;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AdsControlScript: MonoBehaviour{
	
	// Use this for initialization
	public static AdsControlScript adsControl;
	public Text availableHoneyText;
	private InterstitialAd interstitialAd;
	 
	private Reward reward;
	private BannerView bannerView;
	private RewardBasedVideoAd rewardBasedVideo;
	private bool rewardBasedEventHandlersSet;
	private UIManager uiManager;
	void Awake(){


		if (adsControl == null) {
			DontDestroyOnLoad (gameObject);
			adsControl = this;

		}
		else if (adsControl != null) {
			Destroy (gameObject);
		}

		uiManager = gameObject.GetComponent<UIManager> ();
		rewardBasedVideo = RewardBasedVideoAd.Instance;

	}

	public void RequestBanner()
	{
		adsControl = this;
		#if UNITY_ANDROID
		string adUnitId = AdUnits.bannerAdUnitID;
		#elif UNITY_IPHONE
		string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Create a 320x50 banner at the bottom of the screen.
		bannerView = new BannerView (adUnitId, AdSize.Banner, AdPosition.Bottom);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder ()
		//.AddTestDevice("51C8F9628805ADB5153D1CBFE82F59EE")
		.Build ();
		// Load the banner with the request.
		bannerView.LoadAd (request);


	}
	public void RequestInterstitialAD(){

		adsControl = this;
		#if UNITY_ANDROID
		string adUnitId = AdUnits.interstitialAdUnitID;
		#elif UNITY_IPHONE
		string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		//create an insterstial ad

		interstitialAd = new InterstitialAd (adUnitId);
		AdRequest request = new AdRequest.Builder ()
//		.AddTestDevice("51C8F9628805ADB5153D1CBFE82F59EE")
		.Build ();

		//Register for ad events

		interstitialAd.OnAdLoaded += HandleInterstitialADLoaded;
		interstitialAd.OnAdClosed += HandleInterstitialADClosed;
		interstitialAd.LoadAd (request);


	}
	// handle hide / show / destroy banner Ad
	public void hideBannerAd(){
		bannerView.Hide ();
	}
	public void showBannerAd(){
		bannerView.Show ();

	}
	public void DestroyBannerAd(){

		bannerView.Destroy ();

	}
	public void showInterstialAD(){

		if (interstitialAd.IsLoaded ()) {

			interstitialAd.Show ();
		}
	}

	public void DestroyInterstitialAD(){
	if (interstitialAd.IsLoaded ()) {
		interstitialAd.Destroy ();
	}

	}
	#region HandleInterstialAdEvents

	private void HandleInterstitialADLoaded (object sender, EventArgs e)
	{
//		Debug.Log ("ad loaded");
	}

	private void HandleInterstitialADClosed (object sender, EventArgs e)
	{
//		Debug.Log ("ad closed");
		RequestInterstitialAD ();
		}

	#endregion
	

	public void RequestRewardBasedVideo(String androidAdUnitId)
	{
		#if UNITY_ANDROID
		string adUnitId = androidAdUnitId;

		#elif UNITY_IPHONE
		string adUnitId = "INSERT_AD_UNIT_HERE";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		AdRequest request = new AdRequest.Builder().Build();
		rewardBasedVideo.LoadAd(request, adUnitId);

		//register for reward events
		// Reward based video instance is a singleton. Register handlers once to
		// avoid duplicate events.
		if (!rewardBasedEventHandlersSet)
		{

//			 Ad event fired when the rewarded video ad
//			 has been received.
//			rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
//				// has failed to load.
//			rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
//				// is opened.
//			rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
//				// has started playing.
//			rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
				// has rewarded the user.
			rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
				// is closed.
			rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
				// is leaving the application.
//			rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

			rewardBasedEventHandlersSet = true;
		}

	}

	public void showRewardedVideo(){
		if (rewardBasedVideo.IsLoaded ()) {

			rewardBasedVideo.Show ();
		} else {
			uiManager.showToastOnUiThread ("No Video is available at the moment");
		}
	}

	#region HandleReviveRewardVideo

//	public void HandleRewardBasedVideoLoaded (object sender, EventArgs args)
//	{
//		Debug.Log ("HandleRewardBasedVideoLoaded event received.");
//	}
//
//	public void HandleRewardBasedVideoFailedToLoad (object sender, AdFailedToLoadEventArgs args)
//	{
//		Debug.Log ("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
//	}
//
//	public void HandleRewardBasedVideoOpened (object sender, EventArgs args)
//	{
//		Debug.Log ("HandleRewardBasedVideoOpened event received");
//	}
//
//	public void HandleRewardBasedVideoStarted (object sender, EventArgs args)
//	{
//		Debug.Log ("HandleRewardBasedVideoStarted event received");
//	}
//
	public void HandleRewardBasedVideoClosed (object sender, EventArgs args)
	{
		DoOnMainThread.ExecuteOnMainThread.Enqueue (() => {
		if (SceneManager.GetActiveScene ().name == "gameoverscene") {

                SceneManager.LoadSceneAsync("gamescene");
		}
		});


	}

	public void HandleRewardBasedVideoRewarded (object sender, Reward args)
	{
		string type = args.Type;
		double amount = args.Amount;
		DoOnMainThread.ExecuteOnMainThread.Enqueue (() => {
		if (SceneManager.GetActiveScene ().name == "inventoryscene") {

			StartCoroutine(AwardHoney());
		}
		});

	}


	#endregion

	IEnumerator LoadScene(){
		SceneManager.LoadScene ("gamescene");
		yield return null;

	}
	IEnumerator AwardHoney(){

		float availableHoney = PlayerPrefernces.getHoneyAvailable ();
		availableHoney += 5;
		PlayerPrefernces.setHoneyAvailable (availableHoney);
		if (Application.platform == RuntimePlatform.Android) {
			uiManager.showToastOnUiThread ("5 Honey added \n Available Honey" + availableHoney);

		}
		availableHoneyText.text = availableHoney + "";
		yield return null;

	}

	
}