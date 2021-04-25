using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player"){
			other.GetComponent<PlayerController>().Kill();
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player"){
			other.gameObject.GetComponent<PlayerController>().Kill();
		}
	}
}
