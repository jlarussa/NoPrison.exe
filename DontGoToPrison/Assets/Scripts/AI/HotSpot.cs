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

  [SerializeField]
  private int health = 100;

  [SerializeField]
  private int selfDamagePerEnemyHit = 1;

  [SerializeField]
  private bool loseHealthPerEnemy = false;

  bool isRunning = false;

  private List<Collider> enemies = new List<Collider>();
  private void OnTriggerEnter( Collider other )
  {
    if ( PhaseMaster.Current.CurrentPhase == Phase.Building )
    {
      return;
    }

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
      if ( !loseHealthPerEnemy )
      {
        health -= selfDamagePerEnemyHit;  
      }

      while ( enemies.Count > 0 )
      {
        foreach ( Collider enemy in enemies )
        {
          if ( loseHealthPerEnemy )
          {
            health -= selfDamagePerEnemyHit;
          }

          enemyScript = enemy.gameObject.GetComponent<Enemy>();
          enemyScript.UpdateHealth( -damage, Enemy.DamageSource.hotspot );
        }
        yield return waitTime;
      }

      if ( health <= 0 )
      {
        Destroy( gameObject );
      }

      isRunning = false;
    }
  }
}
