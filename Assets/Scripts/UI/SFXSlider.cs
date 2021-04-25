using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour
{
	private Slider slider;

	private void Start() {
		slider = GetComponent<Slider>();

		slider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
	}


	public void OnChange(){
		PlayerPrefs.SetFloat("SFXVolume", slider.value);		
	}
}
