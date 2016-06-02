using UnityEngine;
using System.Collections;

public class MovePanel : MonoBehaviour {

	public GameObject panel;

	private Touch initialTouchPosition;
	private float distance;
	private bool hasSwiped;
	private RectTransform rectTransform;
	void Awake(){
		initialTouchPosition = new Touch ();
		rectTransform = panel.GetComponent<RectTransform> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
	
		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				initialTouchPosition = touch;
			} else if (touch.phase == TouchPhase.Moved && !hasSwiped) {
				float deltaX = initialTouchPosition.position.x - touch.position.x;
				float deltaY = initialTouchPosition.position.y -touch.position.y;
				distance = Mathf.Sqrt ((deltaX * deltaX) + (deltaY * deltaY));
				bool sideWays = Mathf.Abs (deltaX) > Mathf.Abs (deltaY);
				if (distance > 100f) {
					if (sideWays && deltaX > 0) { //swiped left

						movePanelRightClicked ();

					} else if (sideWays && deltaX < 0) {
						movePanelLeftClicked ();

					}

					hasSwiped = true;
				}
			

			} else if (touch.phase == TouchPhase.Ended) {
				initialTouchPosition = new Touch ();
				hasSwiped = false;

			}
		}
	}
	public void movePanelRightClicked(){
		rectTransform.localPosition = new Vector3 (-800, 0, 0);

	}
	public void movePanelLeftClicked(){
		rectTransform.localPosition = new Vector3 (0, 0, 0);

	}
}
