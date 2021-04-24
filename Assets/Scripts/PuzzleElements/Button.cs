using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : PuzzleElement
{
	public void OnButtonTrigger(bool active){
		OnTrigger.Invoke(active);
	}
}
