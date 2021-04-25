using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : PuzzleElement
{
	private Collider2D[] selfCols;
	private SpriteRenderer spriteRenderer;
	public bool playAudioOnOpen = false;
	public AudioClip openAudioClip;

	public void SetActiveAllColliders(bool state){
		foreach(Collider2D col in selfCols){
			col.enabled = state;
		}
	}

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
		selfCols = GetComponents<Collider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();

		OnInputTrigger(false);
	}

	private void DoorOpen(){
		try{
			SetActiveAllColliders(false);
		}
		catch{
			selfCols = GetComponents<Collider2D>();
			spriteRenderer = GetComponent<SpriteRenderer>();
			SetActiveAllColliders(false);
		}

		Color openColor = spriteRenderer.color;
		openColor.a = 0.15f;

		spriteRenderer.color = openColor;

		if(playAudioOnOpen){
			GetComponent<AudioSource>().PlayOneShot(openAudioClip);
		}
	}

	private void DoorClose(){
		try{
			SetActiveAllColliders(true);
		}
		catch{
			selfCols = GetComponents<Collider2D>();
			spriteRenderer = GetComponent<SpriteRenderer>();
			SetActiveAllColliders(true);
		}

		Color closedColor = spriteRenderer.color;
		closedColor.a = 1f;

		spriteRenderer.color = closedColor;
	}
}
