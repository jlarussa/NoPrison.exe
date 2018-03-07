using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent( typeof( Text ) )]
public class SlowTextTyper : MonoBehaviour
{
  public EventHandler FinishedTypingEventHandler;

  [SerializeField]
  private float speed = 0.5f;
  [SerializeField]
  private float punctuationSpeed = 0.5f;

  [SerializeField]
  private string inputText = string.Empty;

  private Text textField = null;

  private char[] punctuation = { '!', '.', '?' };

  // Use this for initialization
  void Start()
  {
    textField = GetComponent<Text>();
    if ( string.IsNullOrEmpty( inputText ) )
    {
      return;
    }

    StartCoroutine( SlowTextDisplay( inputText ) );
  }

  private IEnumerator SlowTextDisplay( string input )
  {
    var waitTime = new WaitForSeconds( speed );
    var punctuationWait = new WaitForSeconds( punctuationSpeed );
    textField.text = string.Empty;
    foreach ( char letter in input )
    {
      textField.text += letter;
      if ( punctuation.Contains( letter ) )
      {
        yield return punctuationWait;
        continue;
      }
      yield return waitTime;

    }
    if ( FinishedTypingEventHandler != null )
    {
      FinishedTypingEventHandler.Invoke( this, null );
    }
  }
}
