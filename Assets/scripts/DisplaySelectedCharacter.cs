using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DisplaySelectedCharacter : MonoBehaviour {

	public GameObject[] currentBeeSelected;
	public Button pauseBtn;
	public GameObject tutorialObject;
	private bool isTutorialComplete;
	private Button tutorialCompleteBtn;

	private GameObject instantiatedCharacter;
	// Use this for initialization
	void Start () {
		isTutorialComplete = false;

	}
	void Awake(){

		pauseBtn.gameObject.SetActive (false);
		Time.timeScale = 0;
		tutorialCompleteBtn=tutorialObject.GetComponentInChildren<Button> ();
		instantiatedCharacter=Instantiate (currentBeeSelected [(int)PlayerPrefernces.getCurrentBeeSelected ()], currentBeeSelected[(int)PlayerPrefernces.getCurrentBeeSelected()].transform.position, Quaternion.identity) as GameObject;
		//instantiatedCharacter.AddComponent<beeMove> ();

	}

	public GameObject getInstantiatedCharacter(){
		return instantiatedCharacter;
	}
	public Vector3 getInstantiatedCharacterPosition(){
		if (instantiatedCharacter != null) {
			return instantiatedCharacter.transform.position;
		}
		return new Vector3 (0, 0, 0);
	}
	public Quaternion getInstantiatedCharacterRotation(){
		if (instantiatedCharacter != null) {
			return instantiatedCharacter.transform.rotation;
		}
		return Quaternion.identity;
	}
}
