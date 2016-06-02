using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class BeeMoveMent : MonoBehaviour {

	public Button leftCircle;
	public Button rightCircle;
    public AudioMixer masterAudioMixer;
	private float leftBorder;
	private float rightBorder;
	private ScreenCoordinates screenCoordinates;
	public GameObject selectedBee;
	private GameObject displayedBee;
	private GameObject hand;
	private GameObject backGround;
	private GameObject obstaclewithHoney;
    private float musicLvl;

    void Awake(){

		GameObject obj = GameObject.FindGameObjectWithTag ("screenInfo");
		screenCoordinates=obj.GetComponent<ScreenCoordinates> ();

		obstaclewithHoney = GameObject.Find ("Obstaclewithhoney");
		obstaclewithHoney.SetActive (false);
		displayedBee=selectedBee.GetComponent<DisplaySelectedCharacter>().getInstantiatedCharacter() as GameObject;

		hand = GameObject.FindGameObjectWithTag ("hand");
		backGround = GameObject.FindGameObjectWithTag ("background");


        masterAudioMixer.SetFloat("musicLvl", 10);
        masterAudioMixer.GetFloat("musicLvl", out musicLvl);
        Debug.Log("audio level" + musicLvl);
    }
    // Use this for initialization
    void Start () {
	
//		PlayerPrefs.DeleteKey ("is_movement_tutorial_complete");

		leftBorder = screenCoordinates.getLeftScreenCoordinate ();
		rightBorder = screenCoordinates.getRightScreenCoordinate ();


		if (PlayerPrefernces.getIsMovementtutorialComplete ()) {
			hand.SetActive (false);
			obstaclewithHoney.SetActive (true);
		}


	}
		
	public void moveBeeLeft(){

		if (hand != null) {
			hand.SetActive (false);
			if (!PlayerPrefernces.getIsMovementtutorialComplete ()) {
				backGround.GetComponent<BackgroundScrolling> ().setScrollSpeed ();
			}
			PlayerPrefernces.SetIsMovementtutorialCompleted (true);
			obstaclewithHoney.SetActive (true);
		}

	}
}
