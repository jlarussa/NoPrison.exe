using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent( typeof( Text ) )]
public class StartPlayPhaseValidator : MonoBehaviour 
{

	private Text playText = null;

  [SerializeField]
  private Button button;

  private bool isValid = true;

	private Dictionary<object, bool> validations = new Dictionary<object, bool>();

  void Awake()
  {
    playText = GetComponent<Text>();
  }

  void Start()
  {
		var enemies = GameObject.FindGameObjectsWithTag( "Enemy" );
		foreach ( GameObject enemyObj in enemies )
		{
			var validator = enemyObj.GetComponent<PathValidatorVisualizer>(); 
			if ( validator ==  null )
			{
				continue;
			}
			validator.PathValidityUpdated += OnPathValidityUpdated;
			validations.Add( enemyObj, true );
		}

    PlacementAmountConfig.Current.PlacementAmountChangedEvent += OnRemainingObjectsUpdated;
  }

  // Hacky: make a real play button you slacker
  // adding to the hackniess! Q.Q
  public void PlayPressed()
  {
    if ( isValid && AreAllEnemiesValidPaths() )
    {
      PhaseMaster.Current.BeginPlayPhase();
    }
  }

	private bool AreAllEnemiesValidPaths()
	{
		foreach ( var key in validations.Keys )
		{
			if ( validations[ key ] == false )
			{
				return false;
			}
		}

		return true;
	}

  void OnPathValidityUpdated( object sender, IsValidPathEventArgs args )
  {
    // TODO: change the color of the button to blue / red to match the validator path?

    validations[ sender ] = args.isValidPath;

		button.interactable = AreAllEnemiesValidPaths();

    playText.color = button.interactable ? button.colors.normalColor : button.colors.disabledColor;
  }

  void OnRemainingObjectsUpdated( object sender, RemainingObjectsChangedArgs args )
  {
    // set the path validity to false until the path is re-evaluated
    //isValid = false;
  }

}
