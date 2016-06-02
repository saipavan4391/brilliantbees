using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour {

	public GameObject infoDialog;

	private Text displayInfoText;

	// Use this for initialization

	void Awake(){
		
		displayInfoText = infoDialog.GetComponentInChildren<Text> ();
	}


	public void DisplayProtectionInfo(){

		if (!infoDialog.activeInHierarchy) {
			infoDialog.SetActive (true);
			displayInfoText.text = "Sheild Duration : \n" +
				"\n" +
				"\n" +
				"Default Bee - 5 Seconds \n" +
				"Bam Bee - 10Seconds \n" +
				"Coal Bee - 20Seconds \n";
		} else {
			infoDialog.SetActive (false);
		}

	}
	public void DisplayMagnetProtectionInfo(){
		if (!infoDialog.activeInHierarchy) {
			infoDialog.SetActive (true);
			displayInfoText.text="Magnet Duration : \n" +
				"\n" +
				"\n" +
				"Default Bee - 5 Seconds \n"+
				"Bam Bee - 10Seconds \n"+
				"Coal Bee - 20Seconds";
		}
		else {
			infoDialog.SetActive (false);
		}
	
	}
	public void DisplayHighSpeedDuration(){

		if (!infoDialog.activeInHierarchy) {
			infoDialog.SetActive (true);
			displayInfoText.text="High Speed Boost:  \n" +
				"\n" +
				"\n"+
				"Default Bee - 5 Seconds  \n"+
				"Bam Bee - 10Seconds \n"+
				"Coal Bee - 20Seconds";
		}
		else {
			infoDialog.SetActive (false);
		}

	}
}
