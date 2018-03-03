using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPointerScript : MonoBehaviour
{

  bool dragging = false;
  [SerializeField]
  private Camera activeCamera = null;
  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if ( Input.GetMouseButtonDown( 0 ) )
    {
      dragging = true;
    }
    if ( Input.GetMouseButtonUp( 0 ) )
    {
      dragging = false;
    }
    RaycastHit rayHit;
    if ( dragging )
    {
      if ( Physics.Raycast( Camera.main.ScreenPointToRay( Input.mousePosition ), out rayHit ) )
      {
        transform.position = rayHit.point;
      }
    }
  }
}
