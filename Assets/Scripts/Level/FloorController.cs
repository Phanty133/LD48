using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
	public GameObject[] views;
	public int curView = 0;
	public GameObject activeViewObject {
		get { return views[curView]; }
	}
	private Camera mainCamera;

	public ViewBounds viewBounds { // x: left, y: right
		get {
			float camWidth = mainCamera.orthographicSize * Screen.width / Screen.height;

			return new ViewBounds(
				activeViewObject.transform.position.x - camWidth,
				activeViewObject.transform.position.x + camWidth
			);
		}
	}

	private void Awake() {
		mainCamera = Camera.main.GetComponent<Camera>();
	}

	public void NextView(){
		if(++curView == 6) curView = 0;
	}

	public void PrevView(){
		if(--curView == -1) curView = 5;
	}

	public void SetView(int newView){
		curView = newView;
	}
}

public class ViewBounds{
	public float left;
	public float right;

	public ViewBounds(float left, float right){
		this.left = left;
		this.right = right;
	}
}
