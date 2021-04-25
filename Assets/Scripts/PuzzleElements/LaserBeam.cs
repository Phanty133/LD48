using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
	public float laserWidth = 0.1f;
	public float maxLaserLen = 8192;
	private Vector2 laserDir;
	public Vector2 basePos;
	private GameObject curInput = null;
	private GameObject ignoreObj = null;
	private float prevRotation;

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player"){
			other.GetComponent<PlayerController>().Kill();
		}
	}

	private void OnTriggerStay2D(Collider2D other) {
		if(other.tag != "Player"){
			FireToDirection(laserDir, ignoreObj);
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		FireToDirection(laserDir, ignoreObj);
	}

	public void FireToDirection(Vector2 dir, GameObject obj = null){
		laserDir = dir;
		if(ignoreObj == null) ignoreObj = obj;
		prevRotation = transform.parent.eulerAngles.z;

		RaycastHit2D[] hitArr = Physics2D.RaycastAll(basePos, dir, maxLaserLen);
		RaycastHit2D? hitq = null;

		foreach(RaycastHit2D h in hitArr){
			if(h.collider.gameObject != obj) {
				hitq = h;
				break;
			}
		}

		float length = hitq != null ? ((RaycastHit2D)hitq).distance : maxLaserLen;
		length = Mathf.Max(length, 0.1f); // Prevents the collider being set to 0 length

		transform.position = (Vector2)basePos + dir * length / 2;
		transform.localScale = new Vector3(laserWidth, length, 0);

		if(hitq == null) return;

		RaycastHit2D hit = (RaycastHit2D)hitq;

		if(curInput != null){
			if(((RaycastHit2D)hit).collider.gameObject != curInput){
				curInput.GetComponent<LaserInput>().Deactivate();
				curInput = null;
			}
		}
		else if(hit.collider.tag == "LaserInput"){
			curInput = hit.collider.gameObject;
			curInput.GetComponent<LaserInput>().Activate();
		}
	}

	private void OnDestroy() {
		if(curInput != null){
			curInput.GetComponent<LaserInput>().Deactivate();
		}
	}

	private void Update() {
		if(transform.parent.eulerAngles.z != prevRotation){
			FireToDirection(transform.parent.up, ignoreObj);
		}
	}
}
