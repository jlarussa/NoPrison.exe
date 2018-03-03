using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour {

	[SerializeField]
	private Transform goal = null;

	private NavMeshAgent agent = null;

	// Use this for initialization
	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();
		agent.destination = goal.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
