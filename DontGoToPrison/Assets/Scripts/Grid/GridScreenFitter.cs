using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScreenFitter : MonoBehaviour
{
  [SerializeField]
  GridLayoutController gridController = null;

  [SerializeField]
  AstarPath pathing = null;

  //broken right now. I need to fix this in the future
  void Start()
  {
    var buttonRenderer = gridController.gridButton.GetComponent<Renderer>();
    var buttonHeight = Mathf.Abs( buttonRenderer.bounds.min.z - buttonRenderer.bounds.max.z );
    var buttonWidth = Mathf.Abs( buttonRenderer.bounds.min.x - buttonRenderer.bounds.max.x );
    int gridHeight = ( int )( Screen.height / buttonHeight );
    int gridWidth = ( int )( Screen.width / buttonWidth );
    gridController.gridDimensions = new Vector2( gridHeight, gridWidth );
    gridController.DrawGrid();

  }

  // Update is called once per frame
  void Update()
  {

  }
}
