using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RemainingObjectsChangedArgs : EventArgs
{
  public RemainingObjectsChangedArgs( string tag, int remaining )
  {
    remainingObjects = remaining;
		this.tag = tag;
  }
  public int remainingObjects;
	public string tag;
}

public class PlacementAmountConfig : Singleton<PlacementAmountConfig> 
{
	private struct MaxAndCurrent
	{
		public int Max;
		public int Current;
	}

	public EventHandler<RemainingObjectsChangedArgs> PlacementAmountChangedEvent;

	[SerializeField]
	private List<GameObject> prefabs;

	[SerializeField]
	private List<int> maxPlacementAmounts;

	Dictionary<string, MaxAndCurrent> prefabAmounts = new Dictionary<string, MaxAndCurrent>();

	// Use this for initialization
	void Start () 
	{
		if ( prefabs == null || maxPlacementAmounts == null || prefabs.Count != maxPlacementAmounts.Count )
		{
			Debug.LogError( "hey you messed up the object amounts" );
		}	

		for ( int i = 0; i < prefabs.Count; i++ )
		{
			prefabAmounts.Add( prefabs[ i ].tag, new MaxAndCurrent { Max = maxPlacementAmounts[ i ], Current = 0 } );
		}

		StartCoroutine( NotifyButtons() );
	}

	private IEnumerator NotifyButtons()
	{
		yield return new WaitForEndOfFrame();
		foreach ( KeyValuePair<string, MaxAndCurrent> kvp in prefabAmounts )
		{
			if ( PlacementAmountChangedEvent != null )
			{
				PlacementAmountChangedEvent.Invoke( this, new RemainingObjectsChangedArgs( tag: kvp.Key, remaining: kvp.Value.Max - kvp.Value.Current ) );
			}
		}
	}

	public bool PrefabHasUsesLeft( GameObject prefab )
	{
			if ( !prefabAmounts.ContainsKey( prefab.tag ) )
			{
				Debug.LogError( "ERROR: tried placing an object the PlacementAmountConfig doesn't know about." );
				return false;
			}

			return prefabAmounts[ prefab.tag ].Current < prefabAmounts[ prefab.tag ].Max;
	}

	public void AdjustPrefabAmount( GameObject prefab, int amount )
	{
		MaxAndCurrent maxAndCurrent;
		if ( !prefabAmounts.TryGetValue( prefab.tag, out maxAndCurrent ) )
			{
				Debug.LogError( "ERROR: tried placing an object the PlacementAmountConfig doesn't know about." );
				return;
			}

		if ( maxAndCurrent.Current + amount >= 0 && maxAndCurrent.Current + amount <= maxAndCurrent.Max )
		{
			prefabAmounts[ prefab.tag ] = new MaxAndCurrent() { Current = prefabAmounts[ prefab.tag ].Current + amount, Max = prefabAmounts[ prefab.tag ].Max };
			if ( PlacementAmountChangedEvent != null )
			{
				PlacementAmountChangedEvent.Invoke( this, new RemainingObjectsChangedArgs( tag: prefab.tag, remaining: prefabAmounts[ prefab.tag ].Max - prefabAmounts[ prefab.tag ].Current ) );
			}
		}

	}
	
}
