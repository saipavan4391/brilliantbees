using UnityEngine;
using System.Collections;

public class GlowHoney : MonoBehaviour {

	Animator animator;
	private GameObject background;
	private float glowHoneyTimer = 3;
	private ObstacleMove obstacleMove;
	private GameObject obstacleSpawner;
	private GameObject collectHoneyTutorial;
	private BackgroundScrolling backgroundScroll;

	void Awake(){
//		PlayerPrefs.DeleteKey ("is_collect_honey_tutorial_complete");
		animator = GetComponent<Animator> ();
		background = GameObject.FindGameObjectWithTag ("background");
		animator.enabled = false;
		obstacleMove = this.transform.parent.gameObject.GetComponent<ObstacleMove> ();
		obstacleSpawner = GameObject.FindGameObjectWithTag ("obstaclespawner");
		obstacleSpawner.SetActive (false);
		collectHoneyTutorial = GameObject.FindGameObjectWithTag ("collecthoney");
		collectHoneyTutorial.SetActive (false);
		backgroundScroll = background.GetComponent<BackgroundScrolling> ();
	}
	
	void Start () {
		if (!PlayerPrefernces.getIsCollectHoneytutorialComplete ()) {
			Debug.Log ("test");
			StartCoroutine (pauseGameForCollectHoneyTutorial ());
		} else {
			obstacleSpawner.SetActive (true);
				
		}

	}

	IEnumerator pauseGameForCollectHoneyTutorial(){

		yield return new WaitForSeconds (1);
		animator.enabled = true;
		obstacleMove.speed = 0;
		collectHoneyTutorial.SetActive (true);
		backgroundScroll.scrollSpeed = 0;
		yield return new WaitForSeconds (2);
		PlayerPrefernces.SetIsCollectHoneytutorialCompleted (true);
		collectHoneyTutorial.SetActive (false);
		obstacleMove.speed = 5;
		backgroundScroll.setScrollSpeed ();
		obstacleSpawner.SetActive (true);

	}
}
