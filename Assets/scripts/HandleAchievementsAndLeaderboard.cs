using UnityEngine;
using System.Collections;
using GooglePlayGames;

public class HandleAchievementsAndLeaderboard : MonoBehaviour {

	public static HandleAchievementsAndLeaderboard Instance;
	void Awake(){
		if (Instance == null) {
			DontDestroyOnLoad (gameObject);
			Instance = this;

		}
		else if (Instance != null) {
			Destroy (gameObject);
		}
	}

	public void UnlockAchievement(string achievementID){
		// unlock achievement (achievementID)
		Social.ReportProgress(achievementID, 100.0f, (bool success) => {
			// handle success or failure
			if(success){
//				Debug.Log("successfully unlocked " + achievementID);
			}
			else if(!success){
//				Debug.Log("failed to unlock " + achievementID);
			}

		});

	}
	public void PostScoreToLeaderboard(float score, string leaderboardID){
		Debug.Log("post to leaderboard called");
		Social.ReportScore((long)score, leaderboardID, (bool success) => {
			// handle success or failure
			if(success){
//				Debug.Log("successfully posted your score" + score);
			}
			else{
//				Debug.Log("failed to posted your score" + score);
			}

		});
	}

}
