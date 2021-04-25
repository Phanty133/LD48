using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
	public GameObject levelControllerObj;
	public float maxGrabDistance = 10f;
	public float maxHoldDistance = 2f;
	public float maxGrabSpeed = 5f;
	public float grabMult = 0.2f;
	public float holdMoveSpeed = 5f;
	public float fireForce = 20f;
	public float releaseDelay = 0.5f; // Delay is applied after release+
	public GameObject holdEffectPrefab;
	public GameObject holdTarget = null;
	public GameObject dragTarget = null;
	private Rigidbody2D targetRb;
	private float releaseTimer = -1f;
	private LevelController levelController;
	private GameObject holdEffectObj;

	public void Grab(){
		if(holdTarget) return;
		if(releaseTimer != -1) return;

		if(!dragTarget){
			Vector3 cursorPos = PlayerController.GetCursorWorldPos();
			Vector2 direction = (cursorPos - transform.position).normalized;
			RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxGrabDistance + maxHoldDistance, LayerMask.GetMask("PhysicsObject"));

			if(hit){
				Debug.DrawRay(transform.position, direction, Color.green, 5);
				dragTarget = hit.collider.gameObject;
				targetRb = dragTarget.GetComponent<Rigidbody2D>();
				targetRb.gravityScale = 0;

				holdEffectObj = Instantiate(holdEffectPrefab, hit.collider.transform.position, hit.collider.transform.rotation, hit.collider.transform);
			}
			else{
				Debug.DrawRay(transform.position, direction, Color.red, 5);
			}
		}
		else{
			float distToTarget = Vector2.Distance(transform.position, dragTarget.transform.position) - maxHoldDistance;

			if(distToTarget > maxGrabDistance) {
				dragTarget = null;
				targetRb = null;
				return;
			}

			if(distToTarget <= maxHoldDistance){
				holdTarget = dragTarget;
				dragTarget = null;
				targetRb.velocity = GetComponent<Rigidbody2D>().velocity;
				
				return;
			}

			Vector2 grabDirection = (transform.position - dragTarget.transform.position).normalized;

			if(targetRb.velocity.magnitude >= maxGrabSpeed) {
				// targetRb.velocity.Set();
				return;
			}

			float extraForce = Mathf.Pow(maxGrabSpeed, -(distToTarget * grabMult) + 2);
			targetRb.AddForce(extraForce * grabDirection);
		}
	}

	public void Fire(){
		if(!holdTarget) return;

		Vector2 cursorDirection = (PlayerController.GetCursorWorldPos() - (Vector2) transform.position).normalized;
		holdTarget = null;
		targetRb.gravityScale = 1;

		targetRb.AddForce(cursorDirection * fireForce, ForceMode2D.Impulse);
		targetRb = null;

		Destroy(holdEffectObj);
	}

	public void Release(){
		if(dragTarget){
			dragTarget = null;	
		}

		if(holdTarget){
			holdTarget = null;
		}

		targetRb.gravityScale = 1;
		targetRb.velocity.Set(0, 0);
		targetRb = null;

		releaseTimer = releaseDelay;
		
		Destroy(holdEffectObj);
	}

	private void Awake() {
		levelController = levelControllerObj.GetComponent<LevelController>();
	}

	private void Update() {
		if(holdTarget){
			Vector2 cursorPos = PlayerController.GetCursorWorldPos();
			Vector2 cursorDirection = (cursorPos - (Vector2) transform.position).normalized;
			float cursorDist = Vector2.Distance(transform.position, cursorPos);
			
			float holdDistance = Mathf.Clamp(cursorDist, 0, maxHoldDistance);
			Vector3 targetPos = transform.position + (Vector3) cursorDirection * holdDistance;
			Vector2 targetPosDirection = (targetPos - holdTarget.transform.position).normalized;
			float distanceToTargetPos = Vector2.Distance(targetPos, holdTarget.transform.position);

			targetRb.velocity = targetPosDirection * holdMoveSpeed * (distanceToTargetPos / holdDistance);

			/* Vector2 cursorPos = PlayerController.GetCursorWorldPos();
			Vector2 cursorDirection = (cursorPos - (Vector2) transform.position).normalized;
			Vector2 cursorPosClampedToLevel = levelController.activeFloor.LevelPositionToWorldPosition(cursorPos);
			float cursorDist = levelController.activeFloor.PositionDistanceInLevel(transform.position, cursorPosClampedToLevel);

			Debug.Log("cursor dist: " + cursorDist);

			float holdDistance = Mathf.Clamp(cursorDist, 0, maxHoldDistance);
			Vector3 targetPos = levelController.activeFloor.LevelPositionToWorldPosition(transform.position + (Vector3) cursorDirection * holdDistance);
			Vector2 targetPosDirection = (targetPos - holdTarget.transform.position).normalized;
			float distanceToTargetPos = levelController.activeFloor.PositionDistanceInLevel(targetPos, holdTarget.transform.position);

			Debug.Log("distToTargetPos: " + distanceToTargetPos);

			if(distanceToTargetPos > 5){
				Debug.Log(holdTarget.transform.position);
			}

			targetRb.velocity = targetPosDirection * holdMoveSpeed * (distanceToTargetPos / holdDistance); */
		}

		if(releaseTimer != -1){
			if(releaseTimer > 0){
				releaseTimer -= Time.deltaTime;
			}
			else{
				releaseTimer = -1;
			}
		}
	}
}
