using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorizontalPagedLayoutGroup : LayoutGroup, ILayoutSelfController
{
  public float spacing = 0;

  [SerializeField]
  private RectTransform viewport;

  private float viewportSize;

  public override void CalculateLayoutInputHorizontal()
  {
    base.CalculateLayoutInputHorizontal();
    if ( viewport == null )
    {
      Debug.LogError( "Viewport is NULL! cannot calculate child size without viewport" );
      return;
    }
    viewportSize = viewport.rect.width;
    float totalSize = padding.left + padding.right + viewportSize * rectChildren.Count;

    if ( rectChildren.Count > 0 )
    {
      totalSize += spacing * ( rectChildren.Count - 1 );
    }

    SetLayoutInputForAxis( totalSize, totalSize, totalSize, 0 );
  }

  public override void CalculateLayoutInputVertical()
  {
    if ( viewport == null )
    {
      Debug.LogError( "Viewport is NULL! cannot calculate child size without viewport" );
      return;
    }
    viewportSize = viewport.rect.height;
    SetLayoutInputForAxis( viewportSize, viewportSize, viewportSize, 1 );

  }

  public override void SetLayoutHorizontal()
  {
    if ( viewport == null )
    {
      Debug.LogError( "Viewport is NULL! cannot calculate child size without viewport" );
      return;
    }

    viewportSize = viewport.rect.width;

    float totalSize = viewportSize * rectChildren.Count;

    if ( rectChildren.Count > 0 )
    {
      totalSize += spacing * ( rectChildren.Count - 1 );
    }

    rectTransform.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, padding.left + padding.right + totalSize );
    m_Tracker.Add( this, rectTransform, DrivenTransformProperties.SizeDelta );

    for ( int i = 0; i < rectChildren.Count; i++ )
    {
      var child = rectChildren[ i ];
      child.SetInsetAndSizeFromParentEdge( RectTransform.Edge.Left, padding.left + i * ( viewportSize + spacing ), viewportSize );
      m_Tracker.Add( this, child, DrivenTransformProperties.SizeDelta | DrivenTransformProperties.AnchoredPosition );
    }
  }

  public override void SetLayoutVertical()
  {
    if ( viewport == null )
    {
      Debug.LogError( "Viewport is NULL! cannot calculate child size without viewport" );
      return;
    }
    viewportSize = viewport.rect.height;

    rectTransform.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical, viewportSize );

    //Subtract the padding
    viewportSize -= ( padding.bottom + padding.top );

    for ( int i = 0; i < rectChildren.Count; i++ )
    {
      var child = rectChildren[ i ];
      child.SetInsetAndSizeFromParentEdge( RectTransform.Edge.Top, padding.top, viewportSize );
    }
  }

}