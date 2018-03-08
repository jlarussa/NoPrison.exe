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
  private bool isEraserMode = false;

  [SerializeField]
  private GameObject prefab = null;

  [SerializeField]
  private List<string> NonErasableObjects = new List<string>();

  private GridGraph graph;

  // This is a set of all locations the player has put stuff.
  private Dictionary<Vector3, GameObject> trackedLocations = new Dictionary<Vector3, GameObject>();

  public EventHandler<RemainingObjectsChangedArgs> CurrentObjectsUpdated;

  private void Start()
  {
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
    if ( !trackedLocations.ContainsKey( snappedLocation )
      || trackedLocations[ snappedLocation ] == null
      || NonErasableObjects.Contains( trackedLocations[ snappedLocation ].tag ) )
    {
      return;
    }

    // get the object and move it far away so it can be destroyed at Unity's own pace.
    // if we leave it here and destroy it, it won't dissapear immediately so rebuilding the pathfinding grid
    // will still think it is there.
    PlacementAmountConfig.Current.AdjustPrefabAmount( trackedLocations[ snappedLocation ], -1 );

    trackedLocations[ snappedLocation ].transform.position = new Vector3( 100000, 100000, 100000 );
    Destroy( trackedLocations[ snappedLocation ] );
    trackedLocations.Remove( snappedLocation );

    RebuildGrid();

    return;
  }

  public bool AddNewTrackedObject( Vector3 snappedLocation, GameObject prefab, Transform parentTransform = null )
  {
    if ( trackedLocations.ContainsKey( snappedLocation ) )
    {
      return false;
    }

    if ( !PlacementAmountConfig.Current.PrefabHasUsesLeft( prefab ) )
    {
      return false;
    }

    PlacementAmountConfig.Current.AdjustPrefabAmount( prefab, 1 );

    GameObject obj = GameObject.Instantiate( prefab, snappedLocation, Quaternion.identity, parentTransform );
    trackedLocations.Add( snappedLocation, obj );

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

    float nodeSize = 1;
    float snapX = Mathf.Round( unsnappedPosition.x / nodeSize ) * nodeSize - 0.5f;
    float snapZ = Mathf.Round( unsnappedPosition.z / nodeSize ) * nodeSize - 0.5f;
    return new Vector3( snapX, 0, snapZ );
  }

  public void SelectPrefab( GameObject selectedPrefab )
  {
    prefab = selectedPrefab;
    isEraserMode = false;
  }

}
