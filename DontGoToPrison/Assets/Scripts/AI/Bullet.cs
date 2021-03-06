﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	[SerializeField]
	private float bulletExpirationTime = 3.0f;

	[SerializeField]
	private int damage = 15;

  [SerializeField]
  private bool wallsBlockShots = false;

	private const string enemyTag = "Enemy";

	private const string wallTag = "Wall";

	private const string borderTag = "Border";

	// Use this for initialization
	void Start () 
	{
		StartCoroutine( DestroyCoroutine() );	
	}

	private IEnumerator DestroyCoroutine()
	{
		yield return new WaitForSeconds( bulletExpirationTime );
		Destroy( this.gameObject );
	}

	private void OnTriggerEnter( Collider other )
  {
    if ( other.gameObject.tag == enemyTag  )
    {
			if ( other.gameObject.GetComponent<Enemy>().UpdateHealth( -damage, Enemy.DamageSource.turret ) )
			{
				Destroy( this.gameObject );
			}
			
			return;
    }

		if ( other.gameObject.tag == borderTag )
		{
			Destroy( this.gameObject );
		}

    if ( other.gameObject.tag == wallTag && wallsBlockShots )
    {
      Destroy( this.gameObject );
    }
  }

}
