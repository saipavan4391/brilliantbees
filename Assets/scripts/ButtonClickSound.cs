using UnityEngine;
using System.Collections;

public class ButtonClickSound : MonoBehaviour {

	public AudioSource audioSource;
	public AudioClip audioClip;

	public void playButtonClip(){
		audioSource.clip = audioClip;
		audioSource.Play ();
	}
}
	