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
		PhaseMaster.Current.Register( this.gameObject, phase );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
