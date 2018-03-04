using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( UnityEngine.AI.NavMeshAgent ) )]
public class Enemy : MonoBehaviour
{
  public enum DamageSource
  {
    hotspot,
    turret
  }

  [SerializeField]
  private int maxHealth = 100;

  [SerializeField]
  private List<DamageSource> immunityList = new List<DamageSource>();

  [SerializeField]
  private int currentHealth = 100;
  private void Awake()
  {
    currentHealth = maxHealth;
  }

  public void UpdateHealth( int change, DamageSource source )
  {
    if ( !immunityList.Contains( source ) )
    {
      currentHealth = Mathf.Max( 0, Mathf.Min( maxHealth, change ) );
    }
    if ( currentHealth == 0 )
    {
      GameObject.Destroy( gameObject );
    }
  }
}
