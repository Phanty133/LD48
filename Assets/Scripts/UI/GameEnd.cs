using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
	public GameObject gameWinPanel;

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player"){
			gameWinPanel.SetActive(true);
			Destroy(other.gameObject);
		}
	}
}
