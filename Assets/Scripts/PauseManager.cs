using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
	public GameObject pauseMenuObj;

	public void pause(){
		Time.timeScale = 0;
		pauseMenuObj.SetActive(true);
	}

	public void unpause(){
		Time.timeScale = 1;
		pauseMenuObj.SetActive(false);
	}

	private void Update() {
		if(Input.GetButtonDown("Pause")){
			if(Time.timeScale == 0){
				unpause();
			}
			else{
				pause();
			}
		}
	}
}
