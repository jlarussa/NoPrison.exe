using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent( typeof( Text ) )]
public class RemainingObjectsDisplay : MonoBehaviour
{
  private Text remainingText = null;

  [SerializeField]
  private string LabelText;

  void Awake()
  {
    remainingText = GetComponent<Text>();
  }

  void Start()
  {
    PlacementAmountConfig.Current.PlacementAmountChangedEvent += OnRemainingObjectsUpdated;
  }

  void OnRemainingObjectsUpdated( object sender, RemainingObjectsChangedArgs args )
  {
    // using the tag of this object to compare against the tag of the instantiated object... yup
    if ( args.tag == this.tag )
    {
      remainingText.text = string.Format( "{0}: {1}", LabelText, args.remainingObjects );
    }
  }
}
