using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
  [SerializeField]
  private SlowTextTyper introStory = null;
  // Use this for initialization
  void Start()
  {
    if ( introStory != null )
    {
      introStory.FinishedTypingEventHandler += WaitForIntro;
    }
    else
    {
      SceneManager.LoadScene( "MainMenu" );
    }
  }

  private void WaitForIntro( object sender, EventArgs args )
  {
    SceneManager.LoadScene( "MainMenu" );

  }
}
