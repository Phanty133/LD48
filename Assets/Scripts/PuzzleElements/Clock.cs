using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : PuzzleElement
{
	public float frequency = 1f; // If -1, take the delay instead
	public float delay = 1f;
	public float holdTime = 1f;
	public float startDelay = 0f;
	private float timer = -1;
	private float stepTime;
	private bool state = false;

	private void Awake() {
		stepTime = frequency == -1 ? delay : 1 / frequency;
		timer = stepTime + startDelay;
	}

	private void LateUpdate() {
		if(timer > 0){
			timer -= Time.deltaTime;
		}
		else if(timer != -1){
			state = !state;
			timer = state ? holdTime : stepTime;

			OnTrigger.Invoke(state);
		}
	}
}
