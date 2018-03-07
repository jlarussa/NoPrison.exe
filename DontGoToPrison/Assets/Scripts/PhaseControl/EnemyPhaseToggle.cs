using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent( typeof( AIPath ) )]
public class EnemyPhaseToggle : PhaseToggle 
{
	[SerializeField]
	private bool DeactivateInBuildPhase = false;

	private AIPath aIPath;

	protected override void ToggleOnStart()
	{
		aIPath = GetComponent<AIPath>();

		// either is remains active in the build phase to draw it's path but doesn't move,
		// or it behaves as it did when we first started working on this where it doesn't
		// activate until the play phase.
		if ( !DeactivateInBuildPhase )
		{
			aIPath.canMove = false;
			PhaseMaster.Current.onPhaseChange += OnPhaseChange;
		}
		else
		{
			base.ToggleOnStart();
		}
	}

	private void OnPhaseChange( Phase phase )
	{
		aIPath.canMove = true;
	}
}
