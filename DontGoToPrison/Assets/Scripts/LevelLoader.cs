using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

  [SerializeField]
  private string sceneName = string.Empty;

  public void LoadLevel()
  {
    if ( sceneName != string.Empty )
    {
      SceneManager.LoadScene( sceneName );
    }
  }
}
