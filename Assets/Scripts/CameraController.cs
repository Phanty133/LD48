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
	private float minCamX;
	private float maxCamX;

	public void OnLevelViewChange(GameObject viewObj, int viewID, int direction){
		transform.position = new Vector3(viewObj.transform.position.x, transform.position.y, transform.position.z);
	}

	private void UpdatePos(){
		float newY = transform.position.y;
		float newX = transform.position.x;

		if(player.transform.position.y > -curDepth){ // If the player is above the theoretical depth of the camera, dont follow the player
			newY = -curDepth;
		}
		else{
			newY = player.transform.position.y + verticalOffsetFromPlayer;
		}
	
		if(player.transform.position.x > minCamX && player.transform.position.x < maxCamX){
			newX = player.transform.position.x;
		}

		Vector2 curVel = new Vector2();
		Vector2 dampedPos = Vector2.SmoothDamp(transform.position, new Vector2(newX, newY), ref curVel, dampTime);
		transform.position = new Vector3(dampedPos.x, dampedPos.y, transform.position.z);
	}

	private void Awake() {
		playerController = player.GetComponent<PlayerController>();

		Camera camera = GetComponent<Camera>();
		float unitWidth = camera.orthographicSize * camera.aspect * 2;

		GameObject[] walls = GameObject.FindGameObjectsWithTag("LevelWall");
		float levelWidth = Vector2.Distance(walls[0].transform.position, walls[1].transform.position);
		float levelOverflow = (levelWidth - unitWidth) / 2 + 1f;
		maxCamX = Mathf.Max(0, levelOverflow);
		minCamX = -maxCamX;
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
