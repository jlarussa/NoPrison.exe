using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderWallCreator : MonoBehaviour
{
  [SerializeField]
  private GameObject NPCWall = null;

  [SerializeField]
  private GameObject PlayerDefault = null;

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
    PlacementTracker.Current.SelectPrefab( NPCWall );
    Rect rectTransform = RectTransformToScreenSpace( ( RectTransform )transform );
    if ( Physics.Raycast( Camera.main.ScreenPointToRay( new Vector3( rectTransform.xMin, rectTransform.yMin ) ), out bottomLeft ) )
    {
      PlacementTracker.Current.TilePressed( bottomLeft.point );
    }
    if ( Physics.Raycast( Camera.main.ScreenPointToRay( new Vector3( rectTransform.xMin, rectTransform.yMax ) ), out topLeft ) )
    {
      PlacementTracker.Current.TilePressed( topLeft.point );
    }
    if ( Physics.Raycast( Camera.main.ScreenPointToRay( new Vector3( rectTransform.xMax, rectTransform.yMin ) ), out bottomRight ) )
    {
      PlacementTracker.Current.TilePressed( bottomRight.point );
    }
    if ( Physics.Raycast( Camera.main.ScreenPointToRay( new Vector3( rectTransform.xMax, rectTransform.yMax ) ), out topRight ) )
    {
      PlacementTracker.Current.TilePressed( topRight.point );
    }

    PlacementTracker.Current.SelectPrefab( PlayerDefault );

  }
  private Rect RectTransformToScreenSpace( RectTransform transform )
  {
    Vector2 size = Vector2.Scale( transform.rect.size, transform.lossyScale );
    Rect rect = new Rect( 0, 0, size.x, size.y );
    return rect;
  }

  //private void StraightBorderLine( Vector3 start, Vector3 end )
  //{
  //  if (start.x == end.x)
  //  {
  //    for 
  //  }
  //}
}
