using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SelectCharacter : MonoBehaviour {

	public Button[] selectButtons;
	public GameObject honeyScoreDisplay;


	private float currentGameObject;

	private bool isBambeeBought;
	private bool isCoalBeeBought;

	private float honeyAvailable;
	private UIManager uManager;
	void Awake(){

//		AdsControlScript.adsControl.DestroyBannerAd ();

		honeyAvailable = PlayerPrefernces.getHoneyAvailable ();
		currentGameObject = PlayerPrefernces.getCurrentBeeSelected ();
		setButtonText();

		//get uimanager component
		uManager=gameObject.GetComponent<UIManager>();

	}
	// Use this for initialization
	void Start () {

		honeyScoreDisplay.GetComponentInChildren<Text> ().text = honeyAvailable + "";


	}
	
	// Update is called once per frame
	void Update () {
		

		//check if bambee is bought
		isBambeeBought = PlayerPrefernces.isBambeeBought ();
		if (!isBambeeBought) {
			if (honeyAvailable > 25) {

				selectButtons [1].GetComponentInChildren<Text> ().text = "Unlock for 25";
				selectButtons [1].interactable = true;
			} else if (honeyAvailable < 25) {
				selectButtons [1].GetComponentInChildren<Text> ().text = "Unlock for 25";
				selectButtons [1].interactable = false;
			}
		}
		//check if coalBee is bought
		isCoalBeeBought = PlayerPrefernces.isCoalBeeBought ();
		if (!isCoalBeeBought) {
			if (honeyAvailable > 100) {

				selectButtons [2].GetComponentInChildren<Text> ().text = "Unlock for 100";
				selectButtons [2].interactable = true;
			} else if (honeyAvailable < 100) {
				selectButtons [2].GetComponentInChildren<Text> ().text = "Unlock for 100";
				selectButtons [2].interactable = false;
			}
		} 
//		else if (isCoalBeeBought && currentGameObject == 2) {
//			selectButtons [2].GetComponentInChildren<Text> ().text = "Selected";
//		} else if (isCoalBeeBought && currentGameObject != 2) {
//			selectButtons [2].GetComponentInChildren<Text> ().text = "Select";
//		}
	}

	public void OnBambeeUnlockClicked(){
		
		if (honeyAvailable > 25 && !isBambeeBought) { //25 honey for bambee
			honeyAvailable -= 25;
//			buyBambeeButton.GetComponentInChildren<Text> ().text = "SelectED";
			PlayerPrefernces.setBambeeBought (true);
			PlayerPrefernces.setCurrentBeeSelected (1); //1 for bambee

			setButtonText();
			//unlock bambee achievement
			if(uManager.IsUserAuthenticatedForPlayServices()){
				HandleAchievementsAndLeaderboard.Instance.UnlockAchievement(GPGSIDs.achievement_unlock_bambee);

			}

		} else if(isBambeeBought){
			
			PlayerPrefernces.setCurrentBeeSelected (1);
			setButtonText();

		}
		PlayerPrefernces.setHoneyAvailable (honeyAvailable);
		honeyScoreDisplay.GetComponentInChildren<Text> ().text = honeyAvailable + "";
	}


	public void OnCoalBeeUnlockClicked(){

		if (honeyAvailable > 100 && !isCoalBeeBought) { //100 honey for coalbee
			honeyAvailable -= 100;
			//buyBambeeButton.GetComponentInChildren<Text> ().text = "SelectED";
			PlayerPrefernces.setCoalBeeBought (true);
			PlayerPrefernces.setCurrentBeeSelected (2); //2 for coalbee
			setButtonText();
			//unlock coalbee achievement
			if (uManager.IsUserAuthenticatedForPlayServices ()) {
				HandleAchievementsAndLeaderboard.Instance.UnlockAchievement (GPGSIDs.achievement_unlock_coalbee);
			} 

		} else if(isCoalBeeBought){
			PlayerPrefernces.setCurrentBeeSelected (2);
			setButtonText();
		}
		PlayerPrefernces.setHoneyAvailable (honeyAvailable);
		honeyScoreDisplay.GetComponentInChildren<Text> ().text = honeyAvailable + "";
	}

	public void OnbeeUnlockClicked(){

		PlayerPrefernces.setCurrentBeeSelected (0); //1 for bambee
		setButtonText();

	} 


	void setButtonText(){
		foreach(Button button in selectButtons){
			if (button.IsInteractable ()) {
				button.GetComponentInChildren<Text>().text="Select";
			}
		}
		selectButtons [(int)PlayerPrefernces.getCurrentBeeSelected()].GetComponentInChildren<Text> ().text = "Selected";
	}
}
