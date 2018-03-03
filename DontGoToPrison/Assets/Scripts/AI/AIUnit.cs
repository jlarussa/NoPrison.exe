using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
	Enemy = 0,
	Player = 1
}

public class AIUnit : MonoBehaviour {

	[SerializeField]
	private Team Team;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
