using UnityEngine;
using System.Collections;

public class Gizmo : MonoBehaviour {

	public float gizmoSize = 0.75f;
	public Color gizmocolor = Color.yellow;

	void OnDrawGizmos(){
		Gizmos.color = gizmocolor;
		Gizmos.DrawWireSphere (transform.position, gizmoSize);
	}
}
