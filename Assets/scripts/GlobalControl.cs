using UnityEngine;
using System.Collections;

public class GlobalControl : MonoBehaviour {


	public static GlobalControl Instance;
	public PlayerCurrentStats playerCurrentStats;
	void Awake(){
		playerCurrentStats = new PlayerCurrentStats ();
		if (Instance == null) {
			DontDestroyOnLoad (gameObject);
			Instance = this;
		} else if (Instance != null) {
			Destroy (gameObject);
		}
	}

}
