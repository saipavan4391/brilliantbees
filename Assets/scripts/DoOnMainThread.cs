using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class DoOnMainThread : MonoBehaviour {

	// Use this for initialization
	public readonly static Queue<Action> ExecuteOnMainThread=new Queue<Action>();
	public void Update()
	{
		// dispatch stuff on main thread
		while (ExecuteOnMainThread.Count > 0)
		{
			ExecuteOnMainThread.Dequeue().Invoke();
		}
	}
}
