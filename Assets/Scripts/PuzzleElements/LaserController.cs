using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : PuzzleElement
{
	public GameObject laserPrefab;
	private bool active = false;

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
		GameObject laserObj = Instantiate(laserPrefab, transform.position, transform.rotation, transform);
		LaserBeam laser = laserObj.GetComponent<LaserBeam>();

		laser.basePos = transform.position;
		laser.FireToDirection(transform.up, gameObject);
	}

	public void KillLaser(){
		Destroy(transform.GetChild(0).gameObject);
	}

	private void Awake() {
		active = startActive;
		if(active) SpawnLaser();
	}
}
