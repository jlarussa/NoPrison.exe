using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementTracker : MonoBehaviour
{
  public class RemainingObjectsChangedArgs : EventArgs
  {
    public RemainingObjectsChangedArgs( int remaining )
    {
      remainingObjects = remaining;
    }
    public int remainingObjects;
  }
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

  [SerializeField]
  private int MaxObjects = 30;
  public int currentObjects
  {
    get { return trackedLocations.Keys.Count; }
  }

  // This is a set of all locations the player has put stuff.
  private Dictionary<Vector2, GameObject> trackedLocations = new Dictionary<Vector2, GameObject>();

  public EventHandler<RemainingObjectsChangedArgs> CurrentObjectsUpdated;

  // Use this for initialization
  void Awake()
  {
    Current = this;
  }

  //100% highly reliable hacks that are definitely not based on a race condition
  private void Start()
  {
    if ( CurrentObjectsUpdated != null )
    {
      CurrentObjectsUpdated.Invoke( this, new RemainingObjectsChangedArgs( MaxObjects - currentObjects ) );
    }
  }

  public bool IsLocationTracked( Vector3 location )
  {
    return trackedLocations.ContainsKey( new Vector2( location.x, location.z ) );
  }

  public bool AddNewTrackedObject( Vector3 location, GameObject prefab, Transform parentTransform = null )
  {
    if ( currentObjects >= MaxObjects )
    {
      return false;
    }

    Vector2 vec2 = new Vector2( location.x, location.z );
    if ( trackedLocations.ContainsKey( vec2 ) )
    {
      return false;
    }

    GameObject obj = GameObject.Instantiate( prefab, location, Quaternion.identity, parentTransform );
    trackedLocations.Add( vec2, obj );
    if ( CurrentObjectsUpdated != null )
    {
      CurrentObjectsUpdated.Invoke( this, new RemainingObjectsChangedArgs( MaxObjects - currentObjects ) );
    }
    return true;
  }
}
