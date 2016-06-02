using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BuyPowers : MonoBehaviour {

	public Button protectionPowerBtn;
	public Button magetsPowerBtn;
	public Button highSpeedPowerBtn;
	public GameObject honeyScoreDisplay;

	private float availableprotectionPower;
	private float availableMagnets;
	private float availableSpeedPower;
	private bool isFirstTime;
	private float honeyAvailable;
	void Awake(){
		honeyAvailable = PlayerPrefernces.getHoneyAvailable ();

		availableprotectionPower = PlayerPrefernces.getProtectionPowerAvailable ();
	
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (honeyAvailable < 10) {
			protectionPowerBtn.interactable = false;
			magetsPowerBtn.interactable = false;
			highSpeedPowerBtn.interactable = false;
		} else if (honeyAvailable >= 10 && honeyAvailable < 25) {
			protectionPowerBtn.interactable = true;
			magetsPowerBtn.interactable = false;
			highSpeedPowerBtn.interactable = false;

		} else if (honeyAvailable >= 25 && honeyAvailable < 50) {
			protectionPowerBtn.interactable = true;
			magetsPowerBtn.interactable = true;
			highSpeedPowerBtn.interactable = false;
		} else if (honeyAvailable > 50) {
			protectionPowerBtn.interactable = true;
			magetsPowerBtn.interactable = true;
			highSpeedPowerBtn.interactable = true;
		}
	}

	public void OnProtectionBtnClicked(){
		if (honeyAvailable >= 10) {
			honeyAvailable -= 10; //10 honey for protection
			PlayerPrefernces.increaseProtectionPower ();
			PlayerPrefernces.setHoneyAvailable (honeyAvailable);
			honeyScoreDisplay.GetComponentInChildren<Text> ().text = honeyAvailable + "";
		}
	}
	public void OnMagnetBtnClicked(){
		if (honeyAvailable >= 25) {
			honeyAvailable -= 25; //25 honey per magnet
			PlayerPrefernces.increaseMagnetsAvailable ();
			PlayerPrefernces.setHoneyAvailable (honeyAvailable);
			honeyScoreDisplay.GetComponentInChildren<Text> ().text = honeyAvailable + "";
		}
	}
	public void OnHighSpeedButtonClicked(){
		if (honeyAvailable >= 50) {
			honeyAvailable -= 50; //50 honey per high speed protection
			PlayerPrefernces.increaseHighSpeedPowerAvailable ();
			PlayerPrefernces.setHoneyAvailable (honeyAvailable);
			honeyScoreDisplay.GetComponentInChildren<Text> ().text = honeyAvailable + "";
		}
	}
}
