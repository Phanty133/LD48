using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : PuzzleElement
{
	private Collider2D selfCol;
	private SpriteRenderer spriteRenderer;

	public void OnInputTrigger(bool active){
		if((active && !startActive) || (!active && startActive)){
			DoorOpen();
		}
		else{
			DoorClose();
		}
		
		OnTrigger.Invoke(active);
	}

	private void Awake() {
		selfCol = GetComponent<Collider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();

		OnInputTrigger(false);
	}

	private void DoorOpen(){
		selfCol.enabled = false;

		Color openColor = spriteRenderer.color;
		openColor.a = 0.15f;

		spriteRenderer.color = openColor;
	}

	private void DoorClose(){
		selfCol.enabled = true;

		Color closedColor = spriteRenderer.color;
		closedColor.a = 1f;

		spriteRenderer.color = closedColor;
	}
}
