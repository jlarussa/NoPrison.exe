using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FollowPointerScript : MonoBehaviour
{

  bool dragging = false;
  [SerializeField]
  private Camera activeCamera = null;
  [SerializeField]
  private LineRenderer lineRenderer = null;
  RaycastHit rayHit;
  RaycastHit prevRayHit;
  [SerializeField]
  private int minSegment = 5;
  [SerializeField]
  private GameObject prefab = null;
  [SerializeField]
  private Canvas mainCanvas = null;
  [SerializeField]
  private GameObject parent = null;

  [SerializeField]
  private int lineRendererYOffset = 1;

  private void Start()
  {
    if ( activeCamera == null
      || lineRenderer == null
      || prefab == null
      || mainCanvas == null )
    {
      Debug.LogError( "Serialized Variable Missing! Script will not function properly" );
    }
  }

  void Update()
  {
    if ( Input.GetMouseButtonDown( 0 ) )
    {
      if ( Physics.Raycast( activeCamera.ScreenPointToRay( Input.mousePosition ), out prevRayHit ) )
      {
        dragging = true;
        lineRenderer.positionCount = 0;
      }
      else
      {
        Debug.LogWarning( "Mouse is Off Screen starting drag!" );
      }
    }
    
    if ( Input.GetMouseButtonUp( 0 ) )
    {
      dragging = false;
      OnDragEnd();
    }

    if (
      dragging
      && Physics.Raycast( activeCamera.ScreenPointToRay( Input.mousePosition ), out rayHit )
      && rayHit.collider.gameObject.layer != 4
      && Vector3.Distance( prevRayHit.point, rayHit.point ) > minSegment )
    {
      lineRenderer.positionCount += 1;
      lineRenderer.SetPosition( lineRenderer.positionCount - 1, rayHit.point + new Vector3( 0, lineRendererYOffset, 0) );
      prevRayHit = rayHit;
      transform.position = rayHit.point;
    }
  }

  private Vector3 GetSnapToGridPosition( Vector3 unsnappedPosition )
  {
    GridGraph graph =  AstarPath.active.graphs[0] as GridGraph;
    float nodeSize = graph.nodeSize;
    float snapX = -0.5f * nodeSize + Mathf.Round( unsnappedPosition.x / nodeSize ) * nodeSize;
    float snapZ = -0.5f * nodeSize + Mathf.Round( unsnappedPosition.z / nodeSize ) * nodeSize;
    return new Vector3( snapX, unsnappedPosition.y, snapZ );
  }

  private void OnDragEnd()
  {
    bool changedPathing = false;
    Transform parentTransform = parent == null ? mainCanvas.transform : parent.transform;
    GridGraph graph =  AstarPath.active.graphs[0] as GridGraph;
    
    // Figure out which tiles the line segment touches
    for ( int i = 0; i < lineRenderer.positionCount; i++ )
    {
      if ( i >= lineRenderer.positionCount - 1 )
      {
        break;
      }

      // Get points along the line segment at regular intervals to figure out which points we're hitting
      Vector3 startPoint = lineRenderer.GetPosition( i );
      Vector3 endPoint = lineRenderer.GetPosition( i + 1 );
      float distance = Vector3.Distance( startPoint, endPoint );
      int numIntervals = Mathf.Max( 3, (int)( distance / graph.nodeSize ) );
      Vector3 fromStartToEnd = endPoint - startPoint;
      fromStartToEnd.Normalize();
      for ( int y = 0; y < numIntervals; y++ )
      {
        Vector3 nextLocation = startPoint + fromStartToEnd * y * graph.nodeSize;
        Vector3 snappedLocation = GetSnapToGridPosition( nextLocation );
        if ( PlacementTracker.Current.AddNewTrackedObject( snappedLocation: snappedLocation, prefab: prefab, parentTransform: parentTransform ) )
        {
          changedPathing = true;
        }
      }
    }

    // If we only tapped, place just one thing.
    if ( lineRenderer.positionCount == 0 )
    {
      RaycastHit hit;
      if ( Physics.Raycast( activeCamera.ScreenPointToRay( Input.mousePosition ), out hit ) )
      {
        Vector3 tappedLocation = GetSnapToGridPosition( hit.point );
        if ( PlacementTracker.Current.AddNewTrackedObject( snappedLocation: GetSnapToGridPosition( hit.point ), prefab: prefab, parentTransform: parentTransform ) )
        {
          changedPathing = true;
        }
      }
    }

    if ( changedPathing )
    {
      // We placed walls, rebuild the navigation
      AstarPath.active.Scan();
    }

    // Finally, reset our line renderer
    lineRenderer.positionCount = 0;
  }

  public void SelectPrefab( GameObject selectedPrefab )
  {
    prefab = selectedPrefab;
  }
}
