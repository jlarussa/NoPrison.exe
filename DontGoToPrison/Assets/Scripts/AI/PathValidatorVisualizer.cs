using Pathfinding;
using System;
using UnityEngine;

public class IsValidPathEventArgs : EventArgs
{
  public bool isValidPath;
  public IsValidPathEventArgs( bool isValidPath ) { this.isValidPath = isValidPath; }
}

public class PathValidatorVisualizer : PathVisualizer
{
  public EventHandler<IsValidPathEventArgs> PathValidityUpdated;

  protected override void OnPathDelegate(Path p)
  {
    base.OnPathDelegate( p );
    if ( PathValidityUpdated != null )
    {
      PathValidityUpdated.Invoke( this, new IsValidPathEventArgs( isPathPossible ) );
    }
  }
}
