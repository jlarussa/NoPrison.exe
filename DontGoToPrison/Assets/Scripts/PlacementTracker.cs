using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementTracker : MonoBehaviour
{
	private static PlacementTracker current;
	public static PlacementTracker Current
	{
		get
		{
			return current;
		}
		set
		{
			if ( current == null )
			{
				current = value;
			}
		}
	}

	// This is a set of all locations the player has put stuff.
  // This should damn well not live here but for now #yolo
  private Dictionary<Vector2, GameObject> trackedLocations = new Dictionary<Vector2, GameObject>();

	// Use this for initialization
	void Start () 
	{
		PlacementTracker.Current = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool IsLocationTracked( Vector3 location )
  {
    return trackedLocations.ContainsKey( new Vector2( location.x, location.z ) );
  }

  public bool AddNewTrackedObject( Vector3 location, GameObject prefab, Transform parentTransform = null )
  {
		Vector2 vec2 = new Vector2( location.x, location.z );
		if ( trackedLocations.ContainsKey( vec2 ) )
		{
			return false;
		}
    
		GameObject obj = GameObject.Instantiate( prefab, location, Quaternion.identity, parentTransform );
		trackedLocations.Add( vec2, obj );
		return true;
  }
}
