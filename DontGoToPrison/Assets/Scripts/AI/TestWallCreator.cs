using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestWallCreator : MonoBehaviour {

	[SerializeField]
	private GameObject spawnMe = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetMouseButtonDown( 0 ) )
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			if ( Physics.Raycast( ray, out hit ) )
			{
				Debug.Log( hit.transform.name );
				Instantiate( spawnMe, new Vector3( hit.point.x, hit.point.y + 1, hit.point.z ), Quaternion.identity );
			}
		}
	}
}
