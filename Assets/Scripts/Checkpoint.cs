using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	public GameObject returnPositionObj;

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player"){
			other.gameObject.GetComponent<PlayerController>().respawnPosition = returnPositionObj.transform.position;
		}
	}
}
