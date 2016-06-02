using UnityEngine;
using System.Collections;

public class ParallaxScrolling : MonoBehaviour {


	public Transform backGround;
	public float smoothingFactor;
	private float parallaxScale;

	private Transform cam;
	private Vector3 previousCamPos;

	void Awake(){
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () {
		
		previousCamPos = cam.position;
		parallaxScale = backGround.position.z - 1;
	}
	
	// Update is called once per frame
	void Update () {
	
		float parallax = (previousCamPos.y - cam.position.y) * parallaxScale;
		float backgroundTargetPosy = backGround.position.y + parallax;
		Vector3 position = new Vector3 (backGround.position.x, backgroundTargetPosy, backGround.position.z);
		backGround.position = Vector3.Lerp (backGround.position, position, smoothingFactor * Time.deltaTime);

		previousCamPos = cam.position;


	}
}
