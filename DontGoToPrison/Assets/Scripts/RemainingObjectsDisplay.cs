﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent( typeof( Text ) )]
public class RemainingObjectsDisplay : MonoBehaviour
{
  private Text remainingText = null;

  [SerializeField]
  private PathValidatorVisualizer validator;

  [SerializeField]
  private Button button;

  private bool isValid = true;

  void Awake()
  {
    remainingText = GetComponent<Text>();
  }

  void Start()
  {
    PlacementTracker.Current.CurrentObjectsUpdated += OnRemainingObjectsUpdated;
    validator.PathValidityUpdated += OnPathValidityUpdated;
  }

  // Hacky: make a real play button you slacker
  // adding to the hackniess! Q.Q
  public void PlayPressed()
  {
    if ( isValid )
    {
      PhaseMaster.Current.BeginPlayPhase();
    }
  }

  void OnPathValidityUpdated( object sender, IsValidPathEventArgs args )
  {
    // TODO: change the color of the button to blue / red to match the validator path?
    button.interactable = args.isValidPath;
    isValid = args.isValidPath;
    remainingText.color = button.interactable ? button.colors.normalColor : button.colors.disabledColor;
  }

  void OnRemainingObjectsUpdated( object sender, RemainingObjectsChangedArgs args )
  {
    // set the path validity to false until the path is re-evaluated
    isValid = false;
    remainingText.text = string.Format( "Remaining: {0}", args.remainingObjects );
  }
}
