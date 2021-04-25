using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
	private Slider slider;

	private void Start() {
		slider = GetComponent<Slider>();

		slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
	}

	public void OnChange(){
		PlayerPrefs.SetFloat("MusicVolume", slider.value);		
	}
}
