using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class Enemy : MonoBehaviour {

	[SerializeField]
	private Transform goal = null;

	private UnityEngine.AI.NavMeshAgent agent = null;

	// Use this for initialization
	void Start () 
	{
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		agent.destination = goal.position;
	}
}
