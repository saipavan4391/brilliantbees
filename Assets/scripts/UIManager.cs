using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames;
public class UIManager : MonoBehaviour {

	public AudioMixer masterAudioMixer;
	public static bool isMusicPlaying;
	public Sprite musicOnSprite;
	public Sprite musicOffSprite;
	public Button volBtn;
	public Button googleServicesBtn;
	public Sprite playSprite;
	public Sprite pauseSprite;
	public Sprite googleServicesOnSprite;
	public Sprite gooleSerivesOffSprite;
	//ui elements
	public Text currentScoreText;
	public GameObject gameOver;
	public Button pauseBtn;
	public Text honeyCollected;
	public GameObject tutorialObject;
	public Button useHoney;
	public GameObject loading;



	private float musicLvl;
	private bool isTutorialComplete;
	private PlayerCurrentStats localStats;

	private bool isRestart;
	private string toastString;
	AndroidJavaObject currentActivity;
	void Awake(){
		//get loading object

		if (loading != null) {
			
			loading.SetActive (false);
		}

		AdsControlScript.adsControl.RequestInterstitialAD ();
		AdsControlScript.adsControl.RequestRewardBasedVideo (AdUnits.reviveRewardVideoAdUnitID);
	//	AdsControlScript.adsControl.RequestRewardBasedVideo (AdUnits.increaseHoneyRewardVideoAdUnitID);
		isRestart = false;
		//local player stats
		localStats = new PlayerCurrentStats ();

		if (SceneManager.GetActiveScene().name.Equals("gameoverscene")) {
			activateGameOverMenu ();
		}
		//set initial google services
		PlayGamesPlatform.Activate();

		if ( googleServicesBtn!= null) {
			if (!Social.localUser.authenticated) {
				if (NetworkConnection.isNetworkAvailable ()) {
					AuthenticateUser ();
				}

			} else if (Social.localUser.authenticated) {
				googleServicesBtn.image.sprite = gooleSerivesOffSprite;

			}
		}

	}


	// Use this for initialization
	void Start () {
		isMusicPlaying = true;
	}

	// Update is called once per frame
	void Update () {

		if (useHoney != null) {
			
			if (localStats.availableHoney > 50) {
				useHoney.gameObject.SetActive (true);
			} else if (localStats.availableHoney < 50) {
				useHoney.gameObject.SetActive (false);
			}

		} 

	}
	public void onPlayButtonClicked(){

		if (SceneManager.GetActiveScene ().Equals (SceneManager.GetSceneByName ("menuscene"))) {
			loading.SetActive (true);

			StartCoroutine(loadLevel("gamescene"));
		}

	} 
	public void OnMenuClicked(){
		if (SceneManager.GetActiveScene ().Equals (SceneManager.GetSceneByName ("gameoverscene"))) {

			resetLocalStats ();
			StartCoroutine(loadLevel("menuscene"));



		}

	}
	public void OnRetryClicked(){

		AdsControlScript.adsControl.showInterstialAD ();
		resetLocalStats ();
//		SceneManager.LoadScene ("gamescene");
		StartCoroutine(loadLevel("gamescene"));


	}
	public void OnVolumeClicked(){
		masterAudioMixer.GetFloat ("musicLvl", out musicLvl);
		if (isMusicPlaying  && musicLvl > 0) {
            if(volBtn!=null)
                volBtn.image.sprite = musicOffSprite;
            stopMusic();
			isMusicPlaying = false;
		} else {
            if(volBtn!=null)
                volBtn.image.sprite = musicOnSprite;
            playMusic();
			isMusicPlaying = true;
		}


	}
	public void onPauseClicked(){
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		if (Time.timeScale == 0) {
			Screen.sleepTimeout = 10;
			pauseBtn.image.sprite = playSprite;

		}
		else if(Time.timeScale == 1){
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			pauseBtn.image.sprite = pauseSprite;
		}
	}
	public void OnShopButtonClicked(){
//		SceneManager.LoadScene ("inventoryscene");
		StartCoroutine(loadLevel("inventoryscene"));
		//Application.LoadLevel ("inventoryscene");

	}
	public void OnRateButtonClicked(){
		if (Application.platform == RuntimePlatform.Android) {
			Application.OpenURL ("market://details?id=com.vgsz.brilliantbees");
		}
	}
	public void OnShowLAClicked(){
//		SceneManager.LoadScene ("leaderboardachievementscene");
		StartCoroutine(loadLevel("leaderboardachievementscene"));

	}
	void playMusic(){
		masterAudioMixer.SetFloat ("musicLvl", 5);
	}
	void stopMusic(){
		masterAudioMixer.SetFloat ("musicLvl", -80);
	}
	public void activateGameOverMenu(){

		if (gameOver != null) {

			localStats.currentScore = GlobalControl.Instance.playerCurrentStats.currentScore;
			localStats.availableHoney = GlobalControl.Instance.playerCurrentStats.availableHoney;
			localStats.currentSpeed = GlobalControl.Instance.playerCurrentStats.currentSpeed;
			//	pauseBtn.gameObject.SetActive (false);
			//			GameObject gameOverMenu=gameOver.transform.Find("gameFailedMenu").gameObject;
			//			GameObject menuBtn = gameOver.transform.Find ("menuButton").gameObject;
			//			GameObject retryBtn = gameOver.transform.Find ("retryButton").gameObject;
			GameObject highScoreobject = gameOver.transform.Find ("highScore").gameObject;
			GameObject scoreobject = gameOver.transform.Find ("currentScore").gameObject;
			//			GameObject gameOverImage = gameOver.transform.Find ("gameOverImage").gameObject;
			//			GameObject highScoreImage = gameOver.transform.Find ("highScoreImage").gameObject;
			//			GameObject currentGameScoreImage = gameOver.transform.Find ("currentGameScore").gameObject;
			Text scoreGameOver=scoreobject.GetComponent<Text> ();
			Text highScoreText = highScoreobject.GetComponent<Text> ();
			float highScore=PlayerPrefernces.LoadHighscore ();
			if (localStats.currentScore > highScore) {
				PlayerPrefernces.saveHighScore (localStats.currentScore);
				highScore = localStats.currentScore;
			}
			//			scoreGameOver.text="" + currentScore;
			scoreGameOver.text=localStats.currentScore+"m";
			highScoreText.text = highScore+"m";
			setHoneyCollectedText (localStats.availableHoney);
			//			gameOverMenu.SetActive (true);
			//			menuBtn.SetActive (true);
			//			retryBtn.SetActive (true);
			//			highScoreobject.SetActive (true);
			//			scoreobject.SetActive (true);
			//			gameOverImage.SetActive (true);
			//			highScoreImage.SetActive (true);
			//			currentGameScoreImage.SetActive (true);
		}

	}
	public void setHoneyCollectedText(float collectedHoney){

		if (honeyCollected != null) {
			honeyCollected.text = collectedHoney+"";
		}

	}

	public void setCurrentScoreText(float currentscore){

		currentScoreText.text = currentscore+"m ";
	}
	public void OnTutorialCompleted(){
		pauseBtn.gameObject.SetActive (true);
		Time.timeScale = 1;
		isTutorialComplete = true;
		tutorialObject.gameObject.SetActive (false);

	}
	public bool isTutorialCompleted(){
		return isTutorialComplete;
	}
	public bool isGamePaused(){
		return Time.timeScale == 0 ? true : false;
	}

	public void OnUseHoneyClicked(){
		if (localStats.availableHoney > 50) {
			localStats.availableHoney -= 50;
			honeyCollected.text = localStats.availableHoney+"";
			PlayerPrefernces.setHoneyAvailable (localStats.availableHoney);
//			SceneManager.LoadScene ("gamescene");
			StartCoroutine(loadLevel("gamescene"));

		}


	}
	public void OnWatchVideoClicked(){
		AdsControlScript.adsControl.showRewardedVideo ();
	}

	public void OnGooglePlayGamesClicked (){

		if (!IsUserAuthenticatedForPlayServices()) {
			// authenticate user:
			AuthenticateUser();

		} else if (IsUserAuthenticatedForPlayServices()) {
			SignoutGoogleServices ();
			googleServicesBtn.image.sprite = googleServicesOnSprite;
			//			isGooglePlayGamesOn = false;
			//			PlayerPrefernces.SetIsGooglePlayServicesActive (isGooglePlayGamesOn);

		}

	}
	void resetLocalStats(){
		GlobalControl.Instance.playerCurrentStats.currentScore = 0;
		GlobalControl.Instance.playerCurrentStats.currentSpeed = 2;

	}

	public void showLeaderboardUI(){
		if (IsUserAuthenticatedForPlayServices()) {
			PlayGamesPlatform.Instance.ShowLeaderboardUI (GPGSIDs.leaderboard_distance_travelled);

		} else{
			// authenticate user:
			AuthenticateUser();

		}
	}


	public bool IsUserAuthenticatedForPlayServices(){
		return Social.localUser.authenticated;
	}

	void AuthenticateUser(){
		Social.localUser.Authenticate((bool success) => {
			// handle success or failure
			if(success){
				if(googleServicesBtn!=null){
					googleServicesBtn.image.sprite = gooleSerivesOffSprite;
				}
				if (Application.platform == RuntimePlatform.Android) {
					showToastOnUiThread ("Signed in suceessfully");
				}


			}
			else if(!success){	

				if (Application.platform == RuntimePlatform.Android) {
					showToastOnUiThread ("failed to connect to google account ");
				}
			}

		});
	}
	public void SignoutGoogleServices(){

		PlayGamesPlatform.Instance.SignOut ();
		if (Application.platform == RuntimePlatform.Android) {
			showToastOnUiThread ("Signed Out Successfully");
		}

	}
	public void ShowAchievementsUI(){
		if (IsUserAuthenticatedForPlayServices ()) {
			Social.ShowAchievementsUI ();
		} else if(!IsUserAuthenticatedForPlayServices()) {
			AuthenticateUser ();
		}

	}
	public void showToastOnUiThread(string toastString){
		AndroidJavaClass UnityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");

		currentActivity = UnityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
		this.toastString = toastString;
		currentActivity.Call ("runOnUiThread", new AndroidJavaRunnable (showToast));

	}
	void showToast(){
		AndroidJavaObject context=currentActivity.Call<AndroidJavaObject>("getApplicationContext");
		AndroidJavaClass Toast = new AndroidJavaClass ("android.widget.Toast");

		AndroidJavaObject javaString = new AndroidJavaObject ("java.lang.String",toastString);

		AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject> ("makeText", context, javaString, Toast.GetStatic<int> ("LENGTH_SHORT"));
		toast.Call ("show");

	}

  
	public IEnumerator loadLevel(string scenename){

        //DestroyAds destroyad = new DestroyAds();
        //destroyad.DestroyBannerAds();
		AsyncOperation async = SceneManager.LoadSceneAsync (scenename);
		if (!async.isDone) {
			loading.SetActive (true);
		}

		yield return (0);
	}
}
