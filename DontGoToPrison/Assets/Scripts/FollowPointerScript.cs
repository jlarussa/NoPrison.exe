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

  // Update is called once per frame
  void Update()
  {
    if ( Input.GetMouseButtonDown( 0 ) )
    {
      if ( Physics.Raycast( Camera.main.ScreenPointToRay( Input.mousePosition ), out prevRayHit ) )
      {
        dragging = true;
        lineRenderer.numPositions = 0;
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
      && Physics.Raycast( Camera.main.ScreenPointToRay( Input.mousePosition ), out rayHit )
      && Vector3.Distance( prevRayHit.point, rayHit.point ) > minSegment )
    {
      lineRenderer.numPositions += 1;
      lineRenderer.SetPosition( lineRenderer.numPositions - 1, rayHit.point );
      prevRayHit = rayHit;
      transform.position = rayHit.point;
    }
  }

  private void OnDragEnd()
  {

  }
}
