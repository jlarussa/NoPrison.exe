using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLayoutController : MonoBehaviour 
{
	[SerializeField]
	private Vector2 gridDimensions = new Vector2( 40, 30 );

  [SerializeField]
  private GameObject gridButton = null;

  [SerializeField]
  private GameObject gridButtonParent = null;

  [SerializeField]
	private Material gridShader = null;

	private AstarPath pathing;

	// Use this for initialization
	void Start ()
  {
	  for ( int i = 0; i < gridDimensions.x * gridDimensions.y; i++ )
    {
      Instantiate( gridButton, gridButtonParent.transform );
    }
	}
	
	// Update is called once per frame
	void Update ()
  {
		
	}
}
