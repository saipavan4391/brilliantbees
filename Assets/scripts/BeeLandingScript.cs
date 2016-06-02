using UnityEngine;
using System.Collections;

public class BeeLandingScript : MonoBehaviour {


	public Transform beePosition;
	public float speed;


	// Update is called once per frame
	void FixedUpdate () {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position,beePosition.position, step);
	}
}
