using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ProgressBar;
public class ActivatePowers : MonoBehaviour {

	public GameObject[] powers;
	public GameObject selectBee;
	private int availableProtectionPower;
	private int availableMagnets;
	private int availableHighSpeedPowers;

	private GameObject instantiatedPower;
	private float remainingTime;
	private Text availableProtectionPowerText;
	private Text availableMagnetsText;
	private Text availableHighSpeedText;

	private BeeMove beeMove;
	private Vector3 powerPosition;
	private Quaternion powerRotation;
	private float currentBeeSelected;
	private Button protectionPowerbtn;
	private Button magnetPowerBtn;
	private Button highspeedBtn;
	private DisplaySelectedCharacter displayChar;
	void Awake(){
		//get available powers
		availableProtectionPower = PlayerPrefernces.getProtectionPowerAvailable ();
		availableMagnets = PlayerPrefernces.getMagnetsAvailable ();
		availableHighSpeedPowers = PlayerPrefernces.getHighSpeedPowerAvailable ();
		//get current selected bee
		currentBeeSelected=PlayerPrefernces.getCurrentBeeSelected();

		//get text components
		availableProtectionPowerText = GameObject.Find ("protectionPower").GetComponentInChildren<Text> ();
		availableMagnetsText = GameObject.Find ("magnetPower").GetComponentInChildren<Text> ();
		availableHighSpeedText = GameObject.Find ("highspeedpower").GetComponentInChildren<Text> ();

		//get buttoncomponents
		protectionPowerbtn=GameObject.Find ("protectionPower").GetComponentInChildren<Button> ();
		magnetPowerBtn=GameObject.Find ("magnetPower").GetComponentInChildren<Button> ();
		highspeedBtn = GameObject.Find ("highspeedpower").GetComponentInChildren<Button> ();
		//get displaycharavter component
		displayChar=selectBee.GetComponent<DisplaySelectedCharacter>();

	}
	// Use this for initialization
	void Start () {
		availableProtectionPowerText.text = availableProtectionPower + "";
		availableMagnetsText.text = availableMagnets + "";
		availableHighSpeedText.text = availableHighSpeedPowers + "";
	}
	
	// Update is called once per frame
	void Update () {
		

		remainingTime -= Time.deltaTime;

		if (remainingTime < 0) {
			Destroy (instantiatedPower);
			PlayerPrefernces.setCurrentActivePower (-1);
		}
	}
	void FixedUpdate(){
		//set power postion
		if (instantiatedPower != null) {
			powerPosition = displayChar.getInstantiatedCharacterPosition ();
			powerRotation = displayChar.getInstantiatedCharacterRotation ();

			instantiatedPower.transform.position= powerPosition;
			instantiatedPower.transform.rotation = powerRotation;

		}
	}
	void LateUpdate(){
		
		if (Time.timeScale == 0 ) {
			protectionPowerbtn.interactable = false;
			magnetPowerBtn.interactable = false;
			highspeedBtn.interactable = false;

		} else {
			protectionPowerbtn.interactable = true;
			magnetPowerBtn.interactable = true;
			highspeedBtn.interactable = true;
		}
		if (displayChar.getInstantiatedCharacter ()==null) {
			//disable power buttons
			protectionPowerbtn.interactable = false;
			magnetPowerBtn.interactable = false;
			highspeedBtn.interactable = false;

			//destroy instantiated power
			Destroy(instantiatedPower);
		}
	}
	public void activateProtectionPower(float protectionTime){
		if (availableProtectionPower > 0) {
			if (currentBeeSelected == 0) {
				protectionTime = 5;
			} else if (currentBeeSelected == 1) {
				protectionTime = 10;
			} else if (currentBeeSelected == 2) {
				protectionTime = 20;
			}
			if (instantiatedPower == null) {
				availableProtectionPower -= 1;
				remainingTime = protectionTime;
				PlayerPrefernces.decreaseProtectionPower ();
				PlayerPrefernces.setCurrentActivePower (0);
				availableProtectionPowerText.text = availableProtectionPower + "";
				instantiatedPower = (GameObject)Instantiate (powers [0], powerPosition, Quaternion.identity);
			}
		}
	}
	public void activateMagnetPower(float magnetPowerTime){
		if (availableMagnets > 0) {
			if (currentBeeSelected == 0) {
				magnetPowerTime = 5;
			} else if (currentBeeSelected == 1) {
				magnetPowerTime = 500;
			} else if (currentBeeSelected == 2) {
				magnetPowerTime = 20;
			}
			if (instantiatedPower == null) {
				availableMagnets -= 1;
				remainingTime = magnetPowerTime;
				PlayerPrefernces.decreaseMagnetsAvailable ();
				PlayerPrefernces.setCurrentActivePower (1);
				availableMagnetsText.text = availableMagnets + "";

//				powers [1].SetActive (true);
				instantiatedPower = (GameObject)Instantiate (powers [1], powerPosition, Quaternion.identity);
			}
		}
	}
	public void activateHighSpeedPower(float highSpeedPowerTime){
		if (availableHighSpeedPowers > 0) {
			if (currentBeeSelected == 0) {
				highSpeedPowerTime = 5;
			} else if (currentBeeSelected == 1) {
				highSpeedPowerTime = 10;
			} else if (currentBeeSelected == 2) {
				highSpeedPowerTime = 20;
			}
			if (instantiatedPower == null) {
				availableHighSpeedPowers -= 1;
				remainingTime = highSpeedPowerTime;
				PlayerPrefernces.decreaseHighSpeedPowerAvailable ();
				PlayerPrefernces.setCurrentActivePower (2);
				availableHighSpeedText.text = availableHighSpeedPowers + "";
				instantiatedPower = (GameObject)Instantiate (powers [2], powerPosition, Quaternion.identity);
			}
		}
	}
}
