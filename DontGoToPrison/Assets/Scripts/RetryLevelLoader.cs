using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryLevelLoader : LevelLoader
{

  public override void LoadLevel()
  {
    SceneManager.LoadScene( PlayerPrefs.GetString( GameController.lastLevelPref ) );
  }
}
