using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPointerScript : MonoBehaviour
{

  bool dragging = false;
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
    if ( dragging )
    {
      transform.position = Input.mousePosition;
    }
  }
}
