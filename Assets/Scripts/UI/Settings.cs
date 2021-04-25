using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
	public GameObject settingsContainer;
	public GameObject mainContainer;

	public void OpenSettings(){
		mainContainer.SetActive(false);
		settingsContainer.SetActive(true);
	}

	public void CloseSettings(){
		settingsContainer.SetActive(false);
		mainContainer.SetActive(true);
	}
}
