using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollToPosition : MonoBehaviour
{

  [SerializeField]
  private ScrollRect scrollRect = null;

  // Use this for initialization
  void Start()
  {
    StartCoroutine( Scroll() );
  }

  private IEnumerator Scroll()
  {
    yield return new WaitForEndOfFrame();
    scrollRect.horizontalNormalizedPosition = ( float )( GameController.Current.triggeredParams ) / ( float )( scrollRect.content.childCount - 1 );
  }
}
