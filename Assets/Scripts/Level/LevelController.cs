using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{
	public FloorController activeFloor;
	public UnityEvent<GameObject, int, int> OnViewChange; // New view, viewID, change direction

	public void NextView(){
		activeFloor.NextView();
		OnViewChange.Invoke(activeFloor.activeViewObject, activeFloor.curView, 1);
	}

	public void PrevView(){
		activeFloor.PrevView();
		OnViewChange.Invoke(activeFloor.activeViewObject, activeFloor.curView, -1);
	}

	public void SetView(int newView){
		activeFloor.SetView(newView);
		OnViewChange.Invoke(activeFloor.activeViewObject, activeFloor.curView, 0);
	}

	private void Start() {
		SetView(0);
	}
}
