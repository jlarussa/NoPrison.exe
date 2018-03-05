using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Wall : MonoBehaviour 
{
	private static string enemyTag = "Enemy";

	private static string wallBreakerName = "WallBreaker";

	private void OnTriggerEnter( Collider other )
  {
    if ( other.gameObject.tag == enemyTag && other.transform.parent.name == wallBreakerName )
    {
			Destroy( this.gameObject );
			AstarPath.active.graphs[ 0 ].Scan();
    }
  }
}
