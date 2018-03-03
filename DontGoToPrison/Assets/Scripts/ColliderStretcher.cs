using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderStretcher : MonoBehaviour
{

  // Use this for initialization
  void Start()
  {
    BoxCollider collider = GetComponent<BoxCollider>();
    RectTransform rect = ( RectTransform )transform;
    collider.size = new Vector3( rect.rect.width, rect.rect.height, 1 );
  }
}
