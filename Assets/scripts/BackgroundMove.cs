using UnityEngine;
using System.Collections;

//public class BackgroundMove : MonoBehaviour {
//	public float scrollSpeed;
//	Vector2 vectorOffset;
//	private Vector3 startPosition;
//	private Vector2 savedOffset;
//	private Renderer Mrenderer;
//	public float tileSizeZ;
//	// Use this for initialization
//	void Start () {
//		Screen.orientation=ScreenOrientation.Portrait;
//		Mrenderer = GetComponent<Renderer> ();
//		startPosition = transform.position;
//		savedOffset = Mrenderer.sharedMaterial.GetTextureOffset ("_MainTex");
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ);
//		transform.position = startPosition + Vector3.forward * newPosition;
//		float offset = Time.time * scrollSpeed;
//		Mrenderer.material.mainTextureOffset = new Vector2 (0, offset);
//		float y = Mathf.Repeat (Time.time * scrollSpeed, 1);
//		vectorOffset = new Vector2 (savedOffset.x,y);
//		Mrenderer.sharedMaterial.SetTextureOffset("_MainTex",vectorOffset);
//	}
//	void OnDisable(){
//		Mrenderer.sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
//	}
//}

public class BackgroundMove : MonoBehaviour 
{

	public float scrollSpeed;
	public float tileSizeZ;
	public float speedIncreaseRate;
	private float timeSinceStarted;
	private Vector3 startPosition;


	private float startTime;
	private float currentTime;
	private const float speedIncreaseTimer=2;
	private float intervalTimer;
	private const float maxSpeed = 8;

	void Awake(){
		intervalTimer = speedIncreaseTimer;
		startTime = Time.time;
		if (!PlayerPrefernces.getIsMovementtutorialComplete ()) {
			
			scrollSpeed = 0;

		} else {
			scrollSpeed=GlobalControl.Instance.playerCurrentStats.currentSpeed;
		}
	}
	void Start () 
	{
		startPosition = transform.position;
	
	}
		

	void Update ()
	{
		currentTime = Time.time;
		intervalTimer -= Time.deltaTime;
		if (intervalTimer < 0 && scrollSpeed <= maxSpeed) {
			scrollSpeed += speedIncreaseRate;
			intervalTimer = speedIncreaseTimer;
		} else if (scrollSpeed > maxSpeed) {
			scrollSpeed = maxSpeed;
		}
		float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition- Vector3.up * newPosition;
	}

	public float getScrollSpeed(){
		return scrollSpeed;
	}
	public void setScrollSpeed(){
		scrollSpeed=GlobalControl.Instance.playerCurrentStats.currentSpeed;
	}
}