using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLayoutController : MonoBehaviour
{
  [SerializeField]
  public Vector2 gridDimensions = new Vector2( 40, 30 );

  [SerializeField]
  public GameObject gridButton = null;

  [SerializeField]
  private GameObject gridButtonParent = null;

  [SerializeField]
  private Material gridShader = null;

  private AstarPath pathing;
  private List<GameObject> GridTiles = new List<GameObject>();

  // Use this for initialization
  void Start()
  {
    DrawGrid();
  }

  // Update is called once per frame
  public void DrawGrid()
  {
    ClearGrid();
    for ( int i = 0; i < gridDimensions.x * gridDimensions.y; i++ )
    {
      GridTiles.Add( Instantiate( gridButton, gridButtonParent.transform ) );
    }
  }
  void ClearGrid()
  {
    foreach ( GameObject tile in GridTiles )
    {
      Destroy( tile );
    }
    GridTiles.Clear();
  }
}
