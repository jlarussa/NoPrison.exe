using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotSpot : MonoBehaviour
{
  private static string enemyTag = "Enemy";
  [SerializeField]
  private float rate = 1f;
  [SerializeField]
  private int damage = 1;

  bool isRunning = false;

  private List<Collider> enemies = new List<Collider>();
  private void OnTriggerEnter( Collider other )
  {
    if ( other.gameObject.tag == enemyTag )
    {
      enemies.Add( other );
      if ( !isRunning )
      {
        StartCoroutine( DamageEnemies() );
      }
    }
  }
  private void OnTriggerExit( Collider other )
  {
    if ( other.gameObject.tag == enemyTag )
    {
      enemies.Remove( other );
    }
  }
  private IEnumerator DamageEnemies()
  {
    if ( !isRunning )
    {
      yield return new WaitForEndOfFrame();
      isRunning = true;
      var waitTime = new WaitForSeconds( rate );
      Enemy enemyScript;
      while ( enemies.Count > 0 )
      {
        foreach ( Collider enemy in enemies )
        {
          enemyScript = enemy.gameObject.GetComponent<Enemy>();
          enemyScript.UpdateHealth( -damage, Enemy.DamageSource.hotspot );
        }
        yield return waitTime;
      }
      isRunning = false;
    }
  }
}
