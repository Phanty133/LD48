using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
	public Vector3 origPos;
	public float width = 35;

	private void Awake() {
		origPos = transform.position;
	}
}
