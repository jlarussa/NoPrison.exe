using Pathfinding;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RemainingObjectsChangedArgs : EventArgs
{
  public RemainingObjectsChangedArgs( int remaining )
  {
    remainingObjects = remaining;
  }
  public int remainingObjects;
}

public class PlacementTracker : Singleton<PlacementTracker>
{
  [SerializeField]
  private int MaxObjects = 30;

  private bool isEraserMode = false;

  [SerializeField]
  private GameObject prefab = null;

  private GridGraph graph;

  public int currentObjects
  {
    get { return trackedLocations.Keys.Count; }
  }

  // This is a set of all locations the player has put stuff.
  private Dictionary<Vector3, GameObject> trackedLocations = new Dictionary<Vector3, GameObject>();

  public EventHandler<RemainingObjectsChangedArgs> CurrentObjectsUpdated;

  private void Start()
  {
    if ( CurrentObjectsUpdated != null )
    {
      CurrentObjectsUpdated.Invoke( this, new RemainingObjectsChangedArgs( MaxObjects - currentObjects ) );
    }
  }

  /// <summary>
  /// All tiles register when the game starts
  /// </summary>
  /// <param name="tileLocation"></param>
  /// <param name="objectAtTile"></param>
  public void RegisterTile( Vector3 tileLocation, GameObject objectAtTile = null )
  {
    trackedLocations[ GetSnapToGridPosition( tileLocation ) ] = objectAtTile;
  }

  /// <summary>
  /// When a tile is pressed, it tells the placement tracker. Then the placement tracker does the correct action.
  /// </summary>
  /// <param name="tileLocation"></param>
  public void TilePressed( Vector3 tileLocation )
  {
    Vector3 snappedLocation = GetSnapToGridPosition( tileLocation );
    if ( isEraserMode )
    {
      Erase( snappedLocation );
      return;
    }

    AddNewTrackedObject( snappedLocation, prefab );
  }

  public void SelectEraser()
  {
    isEraserMode = true;
  }

  private void Erase( Vector3 snappedLocation )
  {
    if ( !trackedLocations.ContainsKey( snappedLocation ) || trackedLocations[ snappedLocation ] == null )
    {
      return;
    }

    Destroy( trackedLocations[ snappedLocation ] );
    trackedLocations.Remove( snappedLocation );
    if ( CurrentObjectsUpdated != null )
    {
      CurrentObjectsUpdated.Invoke( this, new RemainingObjectsChangedArgs( MaxObjects - currentObjects ) );
    }

    RebuildGrid();

    return;
  }

  public bool AddNewTrackedObject( Vector3 snappedLocation, GameObject prefab, Transform parentTransform = null )
  {
    if ( currentObjects >= MaxObjects )
    {
      return false;
    }

    if ( trackedLocations.ContainsKey( snappedLocation ) )
    {
      return false;
    }

    GameObject obj = GameObject.Instantiate( prefab, snappedLocation, Quaternion.identity, parentTransform );
    trackedLocations.Add( snappedLocation, obj );

    if ( CurrentObjectsUpdated != null )
    {
      CurrentObjectsUpdated.Invoke( this, new RemainingObjectsChangedArgs( MaxObjects - currentObjects ) );
    }

    RebuildGrid();

    return true;
  }

  private void RebuildGrid()
  {
    AstarPath.active.Scan();
  }

  private Vector3 GetSnapToGridPosition( Vector3 unsnappedPosition )
  {
    if ( graph == null )
    {
      graph = AstarPath.active.graphs[ 0 ] as GridGraph;
    }

    float nodeSize = graph.nodeSize;
    float snapX = Mathf.Round( unsnappedPosition.x / nodeSize ) * nodeSize;
    float snapZ = Mathf.Round( unsnappedPosition.z / nodeSize ) * nodeSize;
    return new Vector3( snapX, unsnappedPosition.y, snapZ );
  }

  public void SelectPrefab( GameObject selectedPrefab )
  {
    prefab = selectedPrefab;
    isEraserMode = false;
  }

}
