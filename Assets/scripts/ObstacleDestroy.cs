using UnityEngine;
using System.Collections;

public class ObstacleDestroy : MonoBehaviour {

	public float destroyTime = 3f;
	void OnEnable(){
		
		Invoke ("Destroy", destroyTime);
	}
	void Destroy(){
		
//		gameObject.SetActive (false);
		ObjectPoolManager.Instance.Destroy(ParseEnum<PoolType>(gameObject.name),gameObject);
	}
	void OnDisable(){
		
		CancelInvoke ();
	}
	private static T ParseEnum<T>(string value){
		return (T)System.Enum.Parse (typeof(PoolType), value, true); 
	}
}
