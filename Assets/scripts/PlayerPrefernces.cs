using UnityEngine;
using System.Collections;

public static class PlayerPrefernces {


	// Use this for initialization
	public static void saveHighScore(float highScore){
		PlayerPrefs.SetFloat ("high_score", highScore);
		PlayerPrefs.Save ();
	}

	public static float LoadHighscore(){
		if (PlayerPrefs.HasKey ("high_score")) {
			return PlayerPrefs.GetFloat ("high_score");

		}
		return 0;
	}
	public static void setHoneyAvailable(float honeyAvailable){
		PlayerPrefs.SetFloat ("available_honey", honeyAvailable);
	}
	public static float getHoneyAvailable(){
		if (PlayerPrefs.HasKey ("available_honey")) {
			return PlayerPrefs.GetFloat ("available_honey");

		}
		return 0;
	}
	public static void setCurrentBeeSelected(float bee_selected){
		PlayerPrefs.SetFloat ("selected_bee", bee_selected);
	}	
	public static float getCurrentBeeSelected(){
		if (PlayerPrefs.HasKey ("selected_bee")) {
			return PlayerPrefs.GetFloat ("selected_bee");

		}
		return 0;
	}
	public static bool isBambeeBought(){
		if (PlayerPrefs.HasKey ("bambee_bought")) {
			return (PlayerPrefs.GetInt ("bambee_bought") == 1)? true : false;

		}
		return false;
	}

	public static void setBambeeBought(bool value){
		PlayerPrefs.SetInt ("bambee_bought", value == true ? 1 : 0);
	}

	public static bool isCoalBeeBought(){
		if (PlayerPrefs.HasKey ("coalbee_bought")) {
			return (PlayerPrefs.GetInt ("coalbee_bought") == 1)? true : false;

		}
		return false;
	}

	public static void setCoalBeeBought(bool value){
		PlayerPrefs.SetInt ("coalbee_bought", value == true ? 1 : 0);
	}
	public static void setInitialPowers(){
		PlayerPrefs.SetInt ("available_protection_power", 3);
		PlayerPrefs.SetInt ("available_magnet_power", 3);
		PlayerPrefs.SetInt ("available_highspeed_power", 3);
	}
	public static void increaseProtectionPower(){
		if (PlayerPrefs.HasKey ("available_protection_power")) {
			int available_protection=PlayerPrefs.GetInt ("available_protection_power");
			available_protection += 1;
			PlayerPrefs.SetInt ("available_protection_power", available_protection);

		}
	}
	public static void decreaseProtectionPower(){
		if (PlayerPrefs.HasKey ("available_protection_power")) {
			int available_protection=PlayerPrefs.GetInt ("available_protection_power");
			available_protection -= 1;
			PlayerPrefs.SetInt ("available_protection_power", available_protection);

		}
	}
	public static int getProtectionPowerAvailable(){
		if (PlayerPrefs.HasKey ("available_protection_power")) {
			return PlayerPrefs.GetInt ("available_protection_power");
		}
		return 0;
	}
	public static void increaseMagnetsAvailable(){
		if (PlayerPrefs.HasKey ("available_magnet_power")) {
			int available_magnets=PlayerPrefs.GetInt ("available_magnet_power");
			available_magnets += 1;
			PlayerPrefs.SetInt ("available_magnet_power", available_magnets);

		}
	}
	public static void decreaseMagnetsAvailable(){
		if (PlayerPrefs.HasKey ("available_magnet_power")) {
			int available_magnets=PlayerPrefs.GetInt ("available_magnet_power");
			available_magnets -= 1;
			PlayerPrefs.SetInt ("available_magnet_power", available_magnets);

		}
	}
	public static int getMagnetsAvailable(){
		if (PlayerPrefs.HasKey ("available_magnet_power")) {
			return PlayerPrefs.GetInt ("available_magnet_power");
		}
		return 0;
	}
	public static void increaseHighSpeedPowerAvailable(){
		if (PlayerPrefs.HasKey ("available_highspeed_power")) {
			int available_highspeed_powers=PlayerPrefs.GetInt ("available_highspeed_power");
			available_highspeed_powers += 1;
			PlayerPrefs.SetInt ("available_highspeed_power", available_highspeed_powers);

		}
	}
	public static void decreaseHighSpeedPowerAvailable(){
		if (PlayerPrefs.HasKey ("available_highspeed_power")) {
			int available_highspeed_powers=PlayerPrefs.GetInt ("available_highspeed_power");
			available_highspeed_powers -= 1;
			PlayerPrefs.SetInt ("available_highspeed_power", available_highspeed_powers);

		}
	}
	public static int getHighSpeedPowerAvailable(){
		if (PlayerPrefs.HasKey ("available_highspeed_power")) {
			return PlayerPrefs.GetInt ("available_highspeed_power");
		}
		return 0;
	}
	public static void setFirstTime(bool value){
		PlayerPrefs.SetInt ("is_first_time", value == true ? 1 : 0);
	}
	public static bool isFirstTime(){
		if (PlayerPrefs.HasKey ("is_first_time")) {
			return (PlayerPrefs.GetInt ("is_first_time") == 1)? false : true;

		}
		return true;
	}
	public static void setCurrentActivePower(int activePower){
		PlayerPrefs.SetInt ("active_power", activePower);
	}
	public static int getCurrentActivePower(){
		if (PlayerPrefs.HasKey ("active_power")) {
			return PlayerPrefs.GetInt ("active_power");
		}
		return -1;
	}
	public static void SetIsMovementtutorialCompleted(bool value){
		PlayerPrefs.SetInt ("is_movement_tutorial_complete", value == true ? 1 : 0);

	}
	public static bool getIsMovementtutorialComplete(){
		if (PlayerPrefs.HasKey ("is_movement_tutorial_complete")) {
			return (PlayerPrefs.GetInt ("is_movement_tutorial_complete") == 1)? true : false;

		}
		return false;
	}
	public static void SetIsCollectHoneytutorialCompleted(bool value){
		PlayerPrefs.SetInt ("is_collect_honey_tutorial_complete", value == true ? 1 : 0);

	}
	public static bool getIsCollectHoneytutorialComplete(){
		if (PlayerPrefs.HasKey ("is_collect_honey_tutorial_complete")) {
			return (PlayerPrefs.GetInt ("is_collect_honey_tutorial_complete") == 1)? true : false;

		}
		return false;
	}
//	public static void SetIsGooglePlayServicesActive(bool value){
//		
//		PlayerPrefs.SetInt ("is_google_play_services_Active", value == true ? 1 : 0);	
//	}
//	public static bool getIsGooglePlayServicesActive(){
//		if (PlayerPrefs.HasKey ("is_google_play_services_Active")) {
//			return (PlayerPrefs.GetInt ("is_google_play_services_Active") == 1)? true : false;
//			 
//		}
//		return false;
//	}

}
