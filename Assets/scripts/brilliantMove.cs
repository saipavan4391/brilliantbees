using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class brilliantMove : MonoBehaviour {

	public Transform brillantPos;
	public Button playButton;
	public Button rateButton;
	public Button shopButton;
	public Button showLAButton;
	public AudioMixerSnapshot menuMusicSnapshot;
	public AudioMixerSnapshot buttonSnapshot;

	public Button volBtn;
	public Sprite musicOnSprite;
	public Sprite musicOffSprite;
	public GameObject bee;
	private float speed = 500f;
	private bool isLoaded=false;
	private float delayTime=1f;
	private float timer;
	private bool isMusicPlaying;
	public AudioMixer masterMixer;
	private float musicLvl;
	private bool waitActive = false;
	void Awake(){
		isMusicPlaying = UIManager.isMusicPlaying;
		//audioSource.Stop ();
	}
	// Use this for initialization
	void Start () {
		masterMixer.GetFloat ("musicLvl", out musicLvl);
		if (musicLvl < 0) {
			volBtn.image.sprite = musicOffSprite;
		} else {
			volBtn.image.sprite = musicOnSprite;
		}
	//	DisplayAd ();

		timer = delayTime;
		Screen.orientation = ScreenOrientation.Portrait;
	}

	void FixedUpdate () {
		
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, brillantPos.position, step);
		if(transform.position.Equals(brillantPos.position)){
			isLoaded = true;
			timer -= Time.deltaTime;
			if (isLoaded && timer < 0) {
				
				bee.SetActive (true);
				playAudio ();
				if (!playButton.IsActive ()) {
					if (!waitActive) {
						StartCoroutine (wait ());
					}
				}
			}
		}
//		if (Input.GetKeyDown (KeyCode.Escape)) {
//			
//			Application.Quit();
//		}
	}
	void playAudio(){
		menuMusicSnapshot.TransitionTo (0.25f);
	}
	void DisplayAd(){
		
		AdsControlScript.adsControl.RequestBanner ();
	}
	IEnumerator wait(){
		
		yield return new WaitForSeconds (0.75f);
		playButton.gameObject.SetActive (true);
		buttonSnapshot.TransitionTo (0);
		yield return new WaitForSeconds (0.75f);
		shopButton.gameObject.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		rateButton.gameObject.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		showLAButton.gameObject.SetActive (true);
		waitActive = true;
	}
}
