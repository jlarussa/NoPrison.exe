using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLayoutController : MonoBehaviour 
{
	[SerializeField]
	private Vector2 gridDimensions = new Vector2( 40, 30 );

	[SerializeField]
	private Material gridShader = null;

	private AstarPath pathing;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
