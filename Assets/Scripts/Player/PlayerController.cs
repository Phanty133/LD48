
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public bool alive = true;
	public GameObject levelObj;
	private Movement movement;
	private bool plyrInvisible = false;
	private Camera mainCamera;
	private LevelController levelController;
	private GravityGun gravityGun;

	public void OnChangeView(GameObject viewObj, int viewID, int direction){
		if(direction == 1 && viewID == 0){
			transform.position = new Vector3(levelController.activeFloor.viewBounds.left, transform.position.y, transform.position.z);
		}
		else if(direction == -1 && viewID == levelController.activeFloor.views.Length - 1){
			transform.position = new Vector3(levelController.activeFloor.viewBounds.right, transform.position.y, transform.position.z);
		}
		else if(direction == 0){
			// idk do something?
		}
	}

	public static Vector2 GetCursorWorldPos(){
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);	
	}

	private void Awake() {
		movement = GetComponent<Movement>();
		mainCamera = Camera.main.GetComponent<Camera>();
		levelController = levelObj.GetComponent<LevelController>();
		gravityGun = GetComponent<GravityGun>();
	}

	private void OnBecameInvisible() {
		plyrInvisible = true;
	}

	private void OnBecameVisible() {
		plyrInvisible = false;
	}

	private void Update() {
		if(plyrInvisible && alive){
			if(movement.PlayerOnGround() == null) return;
			
			float cameraTop = mainCamera.transform.position.y + mainCamera.orthographicSize;

			if(transform.position.y < cameraTop) return;

			alive = false;
			GetComponent<Movement>().pauseMovement = true;
		}

		if(Input.GetButtonDown("Fire1")){
			gravityGun.Fire();
		}

		if(Input.GetButton("Fire2") && !gravityGun.holdTarget){
			gravityGun.Grab();
		}
		else if(gravityGun.dragTarget){
			gravityGun.Release();
		}

		if(Input.GetButtonDown("Fire2")){
			if(gravityGun.holdTarget){
				Debug.Log("release");
				gravityGun.Release();
			}
		}
	}
	
	private void LateUpdate() {
		ViewBounds viewBounds = levelController.activeFloor.viewBounds;

		if(transform.position.x < viewBounds.left){
			levelController.PrevView();
		}
		else if(transform.position.x > viewBounds.right){
			levelController.NextView();
		}
	}
}
