using Pathfinding;
using System;
using UnityEngine;

public class IsValidPathEventArgs : EventArgs
{
  public bool isValidPath;
  public IsValidPathEventArgs( bool isValidPath ) { this.isValidPath = isValidPath; }
}


[RequireComponent( typeof( Seeker ) ) ]
public class PathValidatorVisualizer : MonoBehaviour
{
  [SerializeField]
  private GameObject player = null;

  [SerializeField]
  private Color validColor = Color.cyan;

  [SerializeField]
  private Color invalidColor = Color.red;

  private Seeker seeker = null;

  private LineRenderer lineRenderer = null;

  private float pathAlphaRate = 0.05f;

  private float pathAlpha = 1.0f;

  public EventHandler<IsValidPathEventArgs> PathValidityUpdated;

  void Start ()
  {
    seeker = GetComponent<Seeker>();
    seeker.pathCallback += OnPathDelegate;
    lineRenderer = GetComponentInChildren<LineRenderer>();
    PhaseMaster.Current.onPhaseChange += OnPhaseChange;
  }

  private void OnPhaseChange( Phase newPhase )
  {
    // stop rebuilding the path once the game starts.
    if ( newPhase == Phase.Play )
    {
      seeker.pathCallback -= OnPathDelegate;
    }
  }

  /// <summary>
  /// Determine if there is a valid path, and draw the path using a line renderer.
  /// </summary>
  /// <param name="p"></param>
  private void OnPathDelegate( Path p )
  {
    GraphNode thisLocation = AstarPath.active.GetNearest( transform.position ).node;
    GraphNode playerLocation = AstarPath.active.GetNearest( player.transform.position ).node;

    bool isPathPossible = PathUtilities.IsPathPossible( thisLocation, playerLocation );
    if ( isPathPossible )
    {
      lineRenderer.material.color = new Color( validColor.r, validColor.g, validColor.b, pathAlpha );
    }
    else
    {
      lineRenderer.material.color = new Color( invalidColor.r, invalidColor.g, invalidColor.b, pathAlpha );
    }

    if ( PathValidityUpdated != null )
    {
      PathValidityUpdated.Invoke( this, new IsValidPathEventArgs( isPathPossible ) );
    }

    var vectorArray = p.vectorPath.ToArray();
    lineRenderer.positionCount = vectorArray.Length;
    lineRenderer.SetPositions( vectorArray );
  }

  private void Update()
  {
    // Let's do some trig to oscillate the alpha!
    pathAlpha = ( float )( 0.3f * Math.Sin( Time.realtimeSinceStartup * 2 ) + 0.7f );
    lineRenderer.material.color = new Color( lineRenderer.material.color.r, lineRenderer.material.color.g, lineRenderer.material.color.b, pathAlpha );
  }

}
