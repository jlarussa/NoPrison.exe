using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLevelLoad : MonoBehaviour
{

  [SerializeField]
  int nextLevelButton = 0;
  // Use this for initialization
  public void LoadLevel()
  {
    GameController.Current.triggerEvent( GameController.TriggerType.LoadLevel, nextLevelButton );
  }
}
