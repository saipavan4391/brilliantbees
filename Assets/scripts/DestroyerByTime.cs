using UnityEngine;
using System.Collections;

public class DestroyerByTime : MonoBehaviour {

	public float lifetime;
	void Start ()
	{
		Destroy (gameObject, lifetime);
	}
}
