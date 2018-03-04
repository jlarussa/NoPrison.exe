using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  public enum TriggerType
  {
    Lose,
    Win,
  }
  public static GameController Current
  {
    get;
    private set;
  }
  private Dictionary<TriggerType, Action> triggeredEventList;

  void Awake()
  {
    //Mark ourself Don't destroy on load.
    //singleton may be dumb, but we'll see
    DontDestroyOnLoad( gameObject );
    if ( Current == null )
    {
      Current = this;
    }
    triggeredEventList = new Dictionary<TriggerType, Action>();
    triggeredEventList[ TriggerType.Lose ] = LoseGame;
    triggeredEventList[ TriggerType.Win ] = WinGame;
  }

  public void triggerEvent( TriggerType trigger )
  {
    if ( triggeredEventList[ trigger ] != null )
    {
      triggeredEventList[ trigger ].Invoke();
    }
  }

  private void LoseGame()
  {
    SceneManager.LoadScene( "MainMenu" );
  }

  private void WinGame()
  {
    SceneManager.LoadScene( "MainMenu" );
  }
}
