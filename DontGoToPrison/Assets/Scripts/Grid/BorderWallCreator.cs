using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderWallCreator : MonoBehaviour
{
  //[SerializeField]
  //private GameObject NPCWall = null;

  private RaycastHit bottomLeft;
  private RaycastHit topLeft;
  private RaycastHit bottomRight;
  private RaycastHit topRight;


  void Start()
  {
    StartCoroutine( CalculateBorder() );
  }
  private IEnumerator CalculateBorder()
  {
    yield return new WaitForEndOfFrame();
    Rect rectTransform = RectTransformToScreenSpace( ( RectTransform )transform );
    if ( Physics.Raycast( Camera.main.ScreenPointToRay( new Vector3( rectTransform.xMin, rectTransform.yMin ) ), out bottomLeft ) )
    {
      PlacementTracker.Current.TilePressed( bottomLeft.point );
    }
    if ( Physics.Raycast( Camera.main.ScreenPointToRay( new Vector3( rectTransform.xMin, rectTransform.yMax ) ), out topLeft ) )
    {
      PlacementTracker.Current.TilePressed( bottomRight.point );
    }
    if ( Physics.Raycast( Camera.main.ScreenPointToRay( new Vector3( rectTransform.xMax, rectTransform.yMin ) ), out bottomRight ) )
    {
      PlacementTracker.Current.TilePressed( topLeft.point );
    }
    if ( Physics.Raycast( Camera.main.ScreenPointToRay( new Vector3( rectTransform.xMax, rectTransform.yMax ) ), out topRight ) )
    {
      PlacementTracker.Current.TilePressed( topRight.point );
    }

  }
  public Rect RectTransformToScreenSpace( RectTransform transform )
  {
    Vector2 size = Vector2.Scale( transform.rect.size, transform.lossyScale );
    Rect rect = new Rect( transform.position.x, Screen.height - transform.position.y, size.x, size.y );
    rect.x -= ( transform.pivot.x * size.x );
    rect.y -= ( ( 1.0f - transform.pivot.y ) * size.y );
    return rect;
  }
}
