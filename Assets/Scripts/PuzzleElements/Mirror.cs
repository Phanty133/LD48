using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : LaserInput
{
	public GameObject laserPrefab;
	private GameObject activeLaser;

	public override void Activate()
	{
		SpawnLaser();
	}

	public override void Deactivate()
	{
		KillLaser();
	}

	public void SpawnLaser(){
		if(transform.childCount == 1) KillLaser();

		activeLaser = Instantiate(laserPrefab, transform.position, transform.rotation, transform);
		LaserBeam laser = activeLaser.GetComponent<LaserBeam>();

		laser.basePos = transform.position;
		laser.FireToDirection(transform.up, gameObject);
	}

	public void KillLaser(){
		Destroy(activeLaser);
	}
}
