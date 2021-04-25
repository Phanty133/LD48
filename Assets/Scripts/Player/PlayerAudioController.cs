using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
	public AudioClip[] audioClips;

	private AudioSource audioSource;

	private void Awake() {
		audioSource = GetComponent<AudioSource>();
	}

	public void PlayClip(int clipID){
		audioSource.PlayOneShot(audioClips[clipID]);
	}
}
