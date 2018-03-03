using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( UnityEngine.AI.NavMeshAgent ) )]
public class Enemy : MonoBehaviour
{

  [SerializeField]
  private int maxHealth = 100;

  private int currentHealth = 100;
  private void Awake()
  {
    currentHealth = maxHealth;
  }

  public void UpdateHealth( int change )
  {
    currentHealth = Mathf.Max( 0, Mathf.Min( maxHealth, change ) );
    if ( currentHealth == 0 )
    {
      GameObject.Destroy( gameObject );
    }
  }
}
