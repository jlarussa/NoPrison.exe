using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase
{
	Building = 0,
	Play = 1,
}

public class PhaseMaster : Singleton<PhaseMaster> 
{
	private List<GameObject> buildPhaseObjects = new List<GameObject>(); 
	private List<GameObject> playPhaseObjects = new List<GameObject>(); 

  public Phase CurrentPhase
  {
    get; private set;
  }

  public delegate void OnPhaseChangeDelegate( Phase newPhase );

  public OnPhaseChangeDelegate onPhaseChange;

  private void OnAwake()
  {
    CurrentPhase = Phase.Building;
  }

  public void BeginPlayPhase()
	{
    CurrentPhase = Phase.Play;
    onPhaseChange( CurrentPhase );
    SetActiveList( buildPhaseObjects, false );
		SetActiveList( playPhaseObjects, true );
	}

	public void Register( GameObject go, Phase phase )
	{
		switch ( phase )
		{
			case Phase.Building:
			{
				buildPhaseObjects.Add( go );
				go.SetActive( true );
				break;
			}
			case Phase.Play:
			{
				playPhaseObjects.Add( go );
				go.SetActive( false );
				break;
			}
		}
	}

	private void SetActiveList( List<GameObject> list, bool value )
	{
		foreach ( GameObject go in list )
		{
			go.SetActive( value );
		}
	}

}
