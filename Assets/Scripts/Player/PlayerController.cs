
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public bool alive = true;
	public GameObject levelObj;
	public GameObject deathPanel;
	public Vector2 respawnPosition;
	private Movement movement;
	private bool plyrInvisible = false;
	private Camera mainCamera;
	private LevelController levelController;
	private GravityGun gravityGun;
	private PlayerAudioController playerAudioController;

	public static Vector2 GetCursorWorldPos(){
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);	
	}

	public void Kill(bool perm = false){
		if(perm){
			gravityGun.enableGravityGun = false;
			alive = false;
			movement.pauseMovement = true;
			deathPanel.SetActive(true);
		}
		else{
			gravityGun.Release();
			transform.position = respawnPosition;
			playerAudioController.PlayClip(3);
		}
	}

	private void Awake() {
		movement = GetComponent<Movement>();
		mainCamera = Camera.main.GetComponent<Camera>();
		levelController = levelObj.GetComponent<LevelController>();
		gravityGun = GetComponent<GravityGun>();
		playerAudioController = GetComponent<PlayerAudioController>();
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

			Kill(true);
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
				gravityGun.Release();
			}
		}
	}
	
	private void LateUpdate() {
		/* ViewBounds viewBounds = levelController.activeFloor.viewBounds;
		Vector3 activeViewPos = levelController.activeFloor.activeViewObject.transform.position;
		float viewWidth = levelController.activeFloor.activeViewObject.GetComponent<ViewController>().width;

		if(transform.position.x < activeViewPos.x - viewWidth / 2){
			levelController.PrevView();
		}
		else if(transform.position.x > activeViewPos.x + viewWidth / 2){
			levelController.NextView();
		} */
	}
}
