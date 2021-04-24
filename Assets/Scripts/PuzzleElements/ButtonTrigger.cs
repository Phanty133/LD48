using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
	public UnityEngine.Events.UnityEvent<bool> OnTrigger;
	public float pressDist = 0.5f;
	private List<GameObject> triggeringObjects = new List<GameObject>();

	private void OnTriggerEnter2D(Collider2D other) {
		if(triggeringObjects.Count == 0) OnTrigger.Invoke(true);

		triggeringObjects.Add(other.gameObject);
	}

	private void OnTriggerExit2D(Collider2D other) {
		triggeringObjects.Remove(other.gameObject);

		if(triggeringObjects.Count == 0) OnTrigger.Invoke(false);
	}
}
