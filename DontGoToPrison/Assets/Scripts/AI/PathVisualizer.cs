using Pathfinding;
using System;
using UnityEngine;


[RequireComponent( typeof( Seeker ) ) ]
public class PathVisualizer : MonoBehaviour
{
  [SerializeField]
  protected GameObject player = null;

  [SerializeField]
  protected Color validColor = Color.cyan;

  [SerializeField]
  protected Color invalidColor = Color.red;

  protected Seeker seeker = null;

  protected LineRenderer lineRenderer = null;

  protected float pathAlphaRate = 0.05f;

  protected float pathAlpha = 1.0f;

	protected bool isPathPossible = true;

  void Start ()
  {
    seeker = GetComponent<Seeker>();
    seeker.pathCallback += OnPathDelegate;
    lineRenderer = GetComponentInChildren<LineRenderer>();
    PhaseMaster.Current.onPhaseChange += OnPhaseChange;
  }

  protected virtual void OnPhaseChange( Phase newPhase )
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
  protected virtual void OnPathDelegate( Path p )
  {
    GraphNode thisLocation = AstarPath.active.GetNearest( transform.position ).node;
    GraphNode playerLocation = AstarPath.active.GetNearest( player.transform.position ).node;

    isPathPossible = PathUtilities.IsPathPossible( thisLocation, playerLocation );
    if ( isPathPossible )
    {
      lineRenderer.material.color = new Color( validColor.r, validColor.g, validColor.b, pathAlpha );
    }
    else
    {
      lineRenderer.material.color = new Color( invalidColor.r, invalidColor.g, invalidColor.b, pathAlpha );
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
