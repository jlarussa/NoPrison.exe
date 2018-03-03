using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
      lineRenderer.SetPosition( lineRenderer.positionCount - 1, rayHit.point + new Vector3(0,1,0) );
      prevRayHit = rayHit;
      transform.position = rayHit.point;
    }
  }

  private void OnDragEnd()
  {
    Transform parentTransform = parent == null ? mainCanvas.transform : parent.transform;
    for ( int i = 0; i < lineRenderer.positionCount; i++ )
    {
      GameObject.Instantiate( prefab, lineRenderer.GetPosition( i ), Quaternion.identity, parentTransform );
    }
    lineRenderer.positionCount = 0;
  }

  public void SelectPrefab( GameObject selectedPrefab )
  {
    prefab = selectedPrefab;
  }
}
