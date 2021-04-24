using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transfer : MonoBehaviour
{
	public UnityEngine.Events.UnityEvent<GameObject> OnEnter;

	private void OnTriggerEnter2D(Collider2D other) {
		OnEnter.Invoke(other.gameObject);
	}
}
