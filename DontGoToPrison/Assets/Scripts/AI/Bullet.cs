using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	[SerializeField]
	private float bulletExpirationTime = 3.0f;

	[SerializeField]
	private int damage = 15;

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
			Destroy( this.gameObject );
			other.gameObject.GetComponent<Enemy>().UpdateHealth( -damage, Enemy.DamageSource.turret );
			return;
    }

		if ( other.gameObject.tag == wallTag || other.gameObject.tag == borderTag )
		{
			Destroy( this.gameObject );
		}
  }

}
