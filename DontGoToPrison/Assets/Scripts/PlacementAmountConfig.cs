using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementAmountConfig : Singleton<PlacementAmountConfig> 
{
	private struct MaxAndCurrent
	{
		public int Max;
		public int Current;
	}

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
		}

	}
	
}
