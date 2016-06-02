using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Parallax scrolling script that should be assigned to a layer
/// </summary>
public class BackgroundScrolling : MonoBehaviour
{
	/// <summary>
	/// Scrolling speed
	/// </summary>
	public Vector2 speed = new Vector2(10, 10);

	public float scrollSpeed;
	/// <summary>
	/// Moving direction
	/// </summary>
	public Vector2 direction = new Vector2(-1, 0);

	/// <summary>
	/// Movement should be applied to camera
	/// </summary>
	public bool isLinkedToCamera = false;

	/// <summary>
	/// 1 - Background is infinite
	/// </summary>
	public bool isLooping = false;

	/// <summary>
	/// 2 - List of children with a renderer.
	/// </summary>
	private List<Transform> backgroundPart;

	private Camera camera;

	public float speedIncreaseRate;
	private float timeSinceStarted;

	private float startTime;
	private float currentTime;
	private const float speedIncreaseTimer=2;
	private float intervalTimer;
	private const float maxSpeed = 8;
	void Awake(){
		camera = Camera.main;
	}
	void Start()
	{
		if (!PlayerPrefernces.getIsMovementtutorialComplete ()) {

			scrollSpeed = 0;

		} else {
			scrollSpeed = GlobalControl.Instance.playerCurrentStats.currentSpeed;
		}
		// For infinite background only
		if (isLooping)
		{
			// Get all the children of the layer with a renderer
			backgroundPart = new List<Transform>();

			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);

				// Add only the visible children
				if (child.GetComponent<Renderer>() != null)
				{
					backgroundPart.Add(child);
				}
			}

			// Sort by position.
			// Note: Get the children from left to right.
			// We would need to add a few conditions to handle
			// all the possible scrolling directions.
			backgroundPart = backgroundPart.OrderBy(
				t => t.position.y
			).ToList();
		}
	}

	void Update()
	{
		currentTime = Time.time;
		intervalTimer -= Time.deltaTime;
		if (intervalTimer < 0 && scrollSpeed <= maxSpeed) {
			scrollSpeed += speedIncreaseRate;
			intervalTimer = speedIncreaseTimer;
		} else if (scrollSpeed > maxSpeed) {
			scrollSpeed = maxSpeed;
		}
		// Movement
		Vector3 movement = new Vector3(
			speed.x * direction.x,
			scrollSpeed * direction.y,
			0);

		movement *= Time.deltaTime;
		transform.Translate(movement);

		// Move the camera
		if (isLinkedToCamera)
		{
			camera.transform.Translate(movement);
		}

		// 4 - Loop
		if (isLooping)
		{
			// Get the first object.
			// The list is ordered from left (x position) to right.
			Transform firstChild = backgroundPart.FirstOrDefault();

			if (firstChild != null)
			{
				// Check if the child is already (partly) before the camera.
				// We test the position first because the IsVisibleFrom
				// method is a bit heavier to execute.
				if (firstChild.position.y < camera.transform.position.y)
				{
					// If the child is already on the left of the camera,
					// we test if it's completely outside and needs to be
					// recycled.
					if (firstChild.GetComponent<Renderer>().IsVisibleFrom(camera) == false)
					{
						// Get the last child position.
						Transform lastChild = backgroundPart.LastOrDefault();
						Vector3 lastPosition = lastChild.transform.position;
						Vector3 lastSize = (lastChild.GetComponent<Renderer>().bounds.max - lastChild.GetComponent<Renderer>().bounds.min);
					
						// Set the position of the recyled one to be AFTER
						// the last child.
						// Note: Only work for horizontal scrolling currently.
						firstChild.position = new Vector3(firstChild.position.x, lastPosition.y+lastSize.y, firstChild.position.z);

						// Set the recycled child to the last position
						// of the backgroundPart list.
						backgroundPart.Remove(firstChild);
						backgroundPart.Add(firstChild);
					}
				}
			}
		}
	}
	public float getScrollSpeed(){
		return scrollSpeed;
	}
	public void setScrollSpeed(){
		scrollSpeed=GlobalControl.Instance.playerCurrentStats.currentSpeed;
	}

}
