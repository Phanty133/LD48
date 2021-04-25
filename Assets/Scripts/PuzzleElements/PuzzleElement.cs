using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PuzzleElement : MonoBehaviour
{
	public UnityEvent<bool> OnTrigger;
	public bool startActive = false;
}
