using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
  public virtual void Destroy()
  {
    GameObject.Destroy(gameObject);
  }
}
