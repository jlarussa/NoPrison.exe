using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent( typeof( Turret ) ) ]
public class TurretPhaseToggle : PhaseToggle 
{

	private Turret turret;

	protected override void ToggleOnStart()
	{
		turret = GetComponent<Turret>();
		PhaseMaster.Current.onPhaseChange += OnPhaseChange;
	}

	private void OnPhaseChange( Phase phase )
	{
		turret.StartShooting();
	}

	/// <summary>
	/// This function is called when the MonoBehaviour will be destroyed.
	/// </summary>
	void OnDestroy()
	{
			PhaseMaster.Current.onPhaseChange -= OnPhaseChange;
	}
}
