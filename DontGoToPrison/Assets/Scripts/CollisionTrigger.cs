using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
  [SerializeField]
  private GameController.TriggerType triggerType = GameController.TriggerType.Lose;
  [SerializeField]
  private string targetTag = string.Empty;

  private void OnTriggerEnter( Collider other )
  {
    if ( !string.IsNullOrEmpty( targetTag ) && other.gameObject.tag == targetTag )
    {
      GameController.Current.triggerEvent( triggerType );
    }
  }
}
