using UnityEngine;
using System.Collections;

public class ScreenCoordinates : MonoBehaviour{

	private float leftBorder;
	private float rightBorder;

	void Awake(){
		float dist = (transform.position - Camera.main.transform.position).z;
		leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
		rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;

	}
	public float getLeftScreenCoordinate(){
		return leftBorder;
	}
	public float getRightScreenCoordinate(){
		return rightBorder;
	}

}
