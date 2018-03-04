using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent( typeof( Text ) )]
public class RemainingObjectsDisplay : MonoBehaviour
{
  private Text remainingText = null;

  void Awake()
  {
    remainingText = GetComponent<Text>();
  }

  void Start()
  {
    PlacementTracker.Current.CurrentObjectsUpdated += OnRemainingObjectsUpdated;
  }

  // Hacky: make a real play button you slacker
  public void PlayPressed()
  {
    PhaseMaster.Current.BeginPlayPhase();
  }

  void OnRemainingObjectsUpdated( object sender, PlacementTracker.RemainingObjectsChangedArgs args )
  {
    remainingText.text = string.Format( "Remaining: {0}", args.remainingObjects );
  }
}
