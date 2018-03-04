using UnityEngine;

public class GridButton : MonoBehaviour
{
  private bool mouseDown = false;

	// Use this for initialization
	void Start ()
  {
    PlacementTracker.Current.RegisterTile( this.gameObject.transform.position );
	}

  // Doing this logic in update because Input.GetMouseButtonDown only works in Update.
  private void Update()
  {
    if ( Input.GetMouseButton( 0 ) )
    {
      mouseDown = true;
    }
    else
    {
      mouseDown = false;
    }
  }

  public void OnButtonPress()
  {
    PlacementTracker.Current.TilePressed( this.gameObject.transform.position );
  }

  public void OnPointerEnter()
  {
    if ( mouseDown )
    {
      PlacementTracker.Current.TilePressed( this.gameObject.transform.position );
    }
  }

}
