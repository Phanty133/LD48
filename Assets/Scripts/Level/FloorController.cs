using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
	public GameObject[] views;
	public int curView = 0;
	public GameObject activeSetObj;
	public GameObject activeViewObject {
		get { return views[curView]; }
	}
	private Camera mainCamera;
	private GravityGun gravityGun;

	public ViewBounds viewBounds { // x: left, y: right
		get { return ViewBoundsByID(curView); }
	}

	private ViewBounds ViewBoundsByID(int viewID){
		float camWidth = mainCamera.orthographicSize * Screen.width / Screen.height;

		return new ViewBounds(
			views[viewID].transform.position.x - camWidth,
			views[viewID].transform.position.x + camWidth
		);
	}

	private void Awake() {
		mainCamera = Camera.main.GetComponent<Camera>();
		gravityGun = GameObject.FindGameObjectWithTag("Player").GetComponent<GravityGun>();
	}

	public void NextView(){
		if(++curView == 6) curView = 0;
		OnViewChange(1);
	}

	public void PrevView(){
		if(--curView == -1) curView = 5;
		OnViewChange(-1);
	}

	public void SetView(int newView){
		curView = newView;
		OnViewChange(0);
	}

	private void OnViewChange(int direction){ // When a view changes, set the adjacent view positions
		// Set previous view set to their original positions

		for(int i = 0; i < activeSetObj.transform.childCount; i++){
			Transform child = activeSetObj.transform.GetChild(i);
			child.position = child.gameObject.GetComponent<ViewController>().origPos;
		}

		GameObject prevView = curView == 0 ? views[views.Length - 1] : views[curView - 1];
		GameObject nextView = curView == views.Length - 1 ? views[0] : views[curView + 1];

		activeViewObject.transform.SetParent(activeSetObj.transform);
		prevView.transform.SetParent(activeSetObj.transform);
		nextView.transform.SetParent(activeSetObj.transform);

		float camWidth = mainCamera.orthographicSize * Screen.width / Screen.height * 2;
		float xOffset = direction * camWidth;

		activeViewObject.transform.position = new Vector3(xOffset, 0, 0);
		prevView.transform.position = new Vector3(xOffset - camWidth, 0, 0);
		nextView.transform.position = new Vector3(xOffset + camWidth, 0, 0);
	}

	public Vector2 LevelPositionToWorldPosition(Vector2 pos){
		ViewBounds view0Bounds = ViewBoundsByID(0);
		ViewBounds view5Bounds = ViewBoundsByID(views.Length - 1);
		Vector2 newPos = pos;

		if(pos.x <= view0Bounds.left){
			float xOverflow = view0Bounds.left - pos.x;
			newPos.x = view5Bounds.right - xOverflow;
		}
		else if(pos.x >= view5Bounds.right){
			float xOverflow = pos.x - view5Bounds.right;
			newPos.x = view0Bounds.left + xOverflow;
		}

		return newPos;
	}

	public float PositionDistanceInLevel(Vector2 pos0, Vector2 pos1){
		ViewBounds view0Bounds = ViewBoundsByID(0);
		ViewBounds view5Bounds = ViewBoundsByID(views.Length - 1);

		float pos0DistToLeftTransfer = Mathf.Abs(pos0.x - view0Bounds.left);
		float pos0DistToRightTransfer = Mathf.Abs(pos0.x - view5Bounds.right);
		float pos0DistToPos1 = Mathf.Abs(pos0.x - pos1.x);
		float distToTransfer = Mathf.Min(pos0DistToLeftTransfer, pos0DistToRightTransfer);

		float pos1DistToLeftTransfer = Mathf.Abs(pos1.x - view0Bounds.left);
		float pos1DistToRightTransfer = Mathf.Abs(pos1.x - view5Bounds.right);

		float distViaPos0Left = pos0DistToLeftTransfer + pos1DistToRightTransfer;
		float distViaPos0Right = pos0DistToRightTransfer + pos1DistToLeftTransfer;

		if(pos0DistToPos1 <= distViaPos0Left && pos0DistToPos1 <= distViaPos0Right) return Vector2.Distance(pos0, pos1);

		float dX = Mathf.Min(distViaPos0Left, distViaPos0Right);
		float dY = Mathf.Abs(pos0.y - pos1.y);

		Debug.Log("pos1DistToLeftTransfer: " + pos1DistToLeftTransfer);
		Debug.Log("pos1DistToRightTransfer: " + pos1DistToRightTransfer);

		return new Vector2(dX, dY).magnitude;
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
