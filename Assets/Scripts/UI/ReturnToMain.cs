using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMain : MonoBehaviour
{
	public void LoadScene(){
		UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
	}
}
