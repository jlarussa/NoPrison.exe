﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

  [SerializeField]
  private GameObject topLevelObject = null;

  private void Awake()
  {
    currentHealth = maxHealth;
    if ( topLevelObject == null )
    {
      topLevelObject = gameObject;
    }
  }

  public void UpdateHealth( int change, DamageSource source )
  {
    if ( !immunityList.Contains( source ) )
    {
      currentHealth = Mathf.Max( 0, Mathf.Min( maxHealth, currentHealth + change ) );
    }
    if ( currentHealth == 0 )
    {
      GameObject.Destroy( topLevelObject );
    }
  }
}
