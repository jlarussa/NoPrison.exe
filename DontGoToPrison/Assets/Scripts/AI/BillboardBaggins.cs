using UnityEngine;

public class BillboardBaggins : MonoBehaviour
{
  void Update()
  {
    Vector3 euler = transform.parent.rotation.eulerAngles;
    transform.rotation = Quaternion.Euler( transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z );
  }
}