using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class HandleBackPressed : MonoBehaviour {

	public GameObject loadingScreen;
	// Use this for initialization
	void Awake(){
		DontDestroyOnLoad (gameObject);
	}

	
	// Update is called once per frame
	void Update () {
		Scene activeScene = SceneManager.GetActiveScene ();
		if (Input.GetKeyDown (KeyCode.Escape)) {

			if (activeScene.name.Equals ("menuscene")) {
				Application.Quit ();
			} else if (activeScene.name.Equals ("gamescene")) {
				if (Time.timeScale == 0) {
					Time.timeScale = 1;
				}
				SceneManager.LoadScene ("menuscene");

			} else if (activeScene.name.Equals ("inventoryscene")) {
				StartCoroutine (loadLevel ("menuscene"));
			} else if (activeScene.name.Equals ("gameoverscene")) {
				StartCoroutine (loadLevel ("menuscene"));
			} else if (activeScene.name.Equals ("leaderboardachievementscene")) {
				StartCoroutine (loadLevel ("menuscene"));
			}
		}

	}
	IEnumerator loadLevel (string sceneName)
	{
		//		loadingScreen.SetActive (true);
		AsyncOperation async = SceneManager.LoadSceneAsync (sceneName);
		while (!async.isDone) {

			if (loadingScreen != null) {
				Debug.Log ("loading");
				loadingScreen.SetActive (true);
			}

			yield return (0);
		}


	}
}
