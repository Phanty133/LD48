using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : PuzzleElement
{
	public GameObject laserPrefab;
	private bool active = false;
	private GameObject activeLaser;

	public void OnInputTrigger(){
		active = !active;

		if(active){
			SpawnLaser();
		}
		else{
			KillLaser();
		}
	}

	public void SpawnLaser(){
		activeLaser = Instantiate(laserPrefab, transform.position, transform.rotation, transform);
		LaserBeam laser = activeLaser.GetComponent<LaserBeam>();

		laser.basePos = transform.position;
		laser.FireToDirection(transform.up, gameObject);
	}

	public void KillLaser(){
		Destroy(activeLaser);
	}

	private void Awake() {
		active = startActive;
		if(active) SpawnLaser();
	}
}
