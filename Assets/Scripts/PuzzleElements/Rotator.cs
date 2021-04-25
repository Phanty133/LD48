using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : PuzzleElement
{
	public RotationType rotationType = RotationType.ADDITIVE;
	public float rotationValue = 90f;
	private float? originalRotation;
	private bool activeRotation = false;

	public void OnInputTrigger(bool active){
		switch(rotationType){
			case RotationType.ADDITIVE:
				transform.eulerAngles += new Vector3(0, 0, rotationValue);
				break;
			case RotationType.SET:
				if(originalRotation == null){
					originalRotation = transform.eulerAngles.z;
					transform.eulerAngles = new Vector3(0, 0, rotationValue);
				}
				else{
					transform.eulerAngles = new Vector3(0, 0, (float)originalRotation);
					originalRotation = null;
				}
				break;
			case RotationType.TIMED:
				activeRotation = active;
				break;
		}
	}

	private void Update() {
		if(activeRotation){
			transform.eulerAngles += new Vector3(0, 0, rotationValue * Time.deltaTime);
		}
	}
}

public enum RotationType{
	ADDITIVE,
	SET,
	TIMED
}
