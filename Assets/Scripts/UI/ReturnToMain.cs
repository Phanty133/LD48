using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMain : MonoBehaviour
{
	public void LoadScene(){
		Time.timeScale = 1;
		UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
	}
}
