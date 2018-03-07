using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseToggle : MonoBehaviour 
{
	[SerializeField]
	private Phase phase = Phase.Building;

	// Use this for initialization
	void Start () 
	{
		ToggleOnStart();
	}
	
	protected virtual void ToggleOnStart()
	{
		PhaseMaster.Current.Register( this.gameObject, phase );
	}
}
