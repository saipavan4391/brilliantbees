using UnityEngine;
using System.Collections;

public class ObstacleMove : MonoBehaviour {

	public float speed;
	private float destroyTime = 3f;
	private Rigidbody2D rb;
	void Awake(){
		rb = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
		Time.fixedDeltaTime = 0.01f;

	}

	void FixedUpdate(){

		transform.Translate(new Vector3(0,-1,0)*speed*Time.deltaTime);

	}
}
