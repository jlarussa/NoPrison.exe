using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{

  void Update()
  {
    if ( transform.childCount == 0 )
    {
      GameController.Current.triggerEvent( GameController.TriggerType.Win, GameController.Current.triggeredParams + 1 );
    }
  }
}
