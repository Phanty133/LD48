using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReceiver : LaserInput
{
	public override void Activate(){
		OnTrigger.Invoke(true);
	}

	public override void Deactivate(){
		OnTrigger.Invoke(false);
	}
}
