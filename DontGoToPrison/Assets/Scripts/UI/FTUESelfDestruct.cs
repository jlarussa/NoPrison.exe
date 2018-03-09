using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUESelfDestruct : SelfDestruct
{
  [SerializeField]
  public GameObject nextPanel = null;

  void Start()
  {
    if (nextPanel != null)
    {
      nextPanel.SetActive(false);
    }
  }
  public override void Destroy()
  {
    if (nextPanel != null)
    {
      nextPanel.SetActive(true);
    }
    base.Destroy();
  }
}
