using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float scrollSpeed = 5f;
	public bool scrollPaused = false;
	public float dampTime = 0.5f;
	public float verticalOffsetFromPlayer = 5f;
	public GameObject player;
	public LevelController levelController;
	private float curDepth = 0f;
	private PlayerController playerController;

	public void OnLevelViewChange(GameObject viewObj, int viewID, int direction){
		transform.position = new Vector3(viewObj.transform.position.x, transform.position.y, transform.position.z);
	}

	private void UpdatePos(){
		if(player.transform.position.y > -curDepth){ // If the player is above the theoretical depth of the camera, dont follow the player
			transform.position = new Vector3(transform.position.x, -curDepth, transform.position.z);
		}
		else{
			Vector2 curVel = new Vector2();
			Vector2 dampedPos = Vector2.SmoothDamp(transform.position, (Vector2)player.transform.position + new Vector2(0, verticalOffsetFromPlayer), ref curVel, dampTime);
		
			transform.position = new Vector3(transform.position.x, dampedPos.y, transform.position.z);
		}
	}

	private void Awake() {
		playerController = player.GetComponent<PlayerController>();
	}

	private void LateUpdate() {
		if(!scrollPaused){
			curDepth += scrollSpeed * Time.deltaTime;
		}

		UpdatePos();

		if(!playerController.alive){
			scrollPaused = true;
		}
	}
}
