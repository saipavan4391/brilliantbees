using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class VolumeSettings : MonoBehaviour ,IPointerClickHandler{

	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData)
	{
		isOpen = !isOpen;
		runUpdate = true;
	}
	#endregion


	public RectTransform container;
	public bool isOpen;
	private Vector3 scale;
	private bool runUpdate;
	void Awake(){
		container = transform.FindChild ("container").GetComponent<RectTransform> ();
		isOpen = false;
	}
	// Use this for initialization
	void Start () {

		scale = container.localScale;
		scale.y = 0;
		container.localScale = scale;
		runUpdate = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (runUpdate) {
			scale.y = Mathf.Lerp (scale.y, isOpen ? 1 : 0, Time.deltaTime * 12);
			container.localScale = scale;

		}

	}

//	void OpenSettings(){
//		
//		while (true) {
//			if (scale.y < 0) {
//				break;
//			}
//			scale.y = Mathf.Lerp (scale.y, isOpen ? 1 : 0, Time.deltaTime * 12);
//			container.localScale = scale;
//		}
//	}
}
	