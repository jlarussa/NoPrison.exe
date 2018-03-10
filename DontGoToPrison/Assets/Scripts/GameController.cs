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
    LoadLevel
  }
  public static GameController Current
  {
    get;
    private set;
  }
  private Dictionary<TriggerType, Action> triggeredEventList;

  public int triggeredParams = 0;

  public const string lastLevelPref = "LastScene";

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

  //You will always set the parameters here if you want to
  public void triggerEvent( TriggerType trigger, int parameters )
  {
    triggeredParams = parameters;

    if ( triggeredEventList.ContainsKey( trigger ) && triggeredEventList[ trigger ] != null )
    {
      triggeredEventList[ trigger ].Invoke();
    }
  }

  private void LoseGame()
  {
    PlayerPrefs.SetString( lastLevelPref, SceneManager.GetActiveScene().name );
    SceneManager.LoadScene( "GameOver" );
  }

  private void WinGame()
  {
    SceneManager.LoadScene( "WinGame" );
  }
}
