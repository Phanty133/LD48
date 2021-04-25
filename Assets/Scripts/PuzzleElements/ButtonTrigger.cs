using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
	public UnityEngine.Events.UnityEvent<bool> OnTrigger;
	public float pressDist = 0.5f;
	public float pressedButtonDepth = 0.2f;
	public float changeStateTime = 0.25f;
	private List<GameObject> triggeringObjects = new List<GameObject>();
	private Vector2 origPos;
	private bool changeState = false;
	private Vector2 targetPos;
	private float stateTimer = 0;
	private Vector2 stateStartPos;

	private void OnTriggerEnter2D(Collider2D other) {
		if(triggeringObjects.Count == 0) OnTrigger.Invoke(true);
		UpdateButtonVisual(true);

		triggeringObjects.Add(other.gameObject);
	}

	private void OnTriggerExit2D(Collider2D other) {
		triggeringObjects.Remove(other.gameObject);
		UpdateButtonVisual(false);

		if(triggeringObjects.Count == 0) OnTrigger.Invoke(false);
	}

	private void UpdateButtonVisual(bool state){
		changeState = true;
		stateStartPos = transform.localPosition;

		if(state){
			targetPos = transform.localPosition - new Vector3(0, 0.2f, 0);
		}
		else{
			targetPos = origPos;
		}
	}

	private void Awake() {
		origPos = transform.localPosition;
	}

	private void Update() {
		if(changeState){
			stateTimer += Time.deltaTime;
			
			transform.localPosition = Vector2.Lerp(stateStartPos, targetPos, stateTimer / changeStateTime);

			if(stateTimer >= changeStateTime){
				stateTimer = 0;
				changeState = false;
			}
		}
	}
}
