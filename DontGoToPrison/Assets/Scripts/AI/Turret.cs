using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Turret : MonoBehaviour
{
  [SerializeField]
  private GameObject enemyParent = null;

  [SerializeField]
  private float targetInterval = 0.25f;

  [SerializeField]
  private GameObject bullet = null;

  [SerializeField]
  private float bulletSpeed = 12f;

  private Transform target = null;

  // Use this for initialization
  void Start()
  {
    if (enemyParent == null)
    {
      enemyParent = PlacementTracker.Current.EnemyParent;
    }

    if (bullet == null)
    {
      Debug.LogError("Error: turret needs a bullet prefab");
    }
  }

  public void StartShooting()
  {
    StartCoroutine(FindTargetCoroutine());
  }

  private IEnumerator FindTargetCoroutine()
  {
    while (true)
    {
      Transform child;
      Transform bestTarget = null;
      float closestDistance = Mathf.Infinity;
      for (int i = 0; i < enemyParent.transform.childCount; i++)
      {
        child = enemyParent.transform.GetChild(i);
        float distance = Vector3.Distance(transform.position, child.position);

        // TODO: determine line of sight before deciding on target

        if (distance < closestDistance)
        {
          closestDistance = distance;
          bestTarget = child;
        }
      }

      target = bestTarget;

      ShootAtTarget();

      yield return new WaitForSeconds(targetInterval);
    }
  }

  private void ShootAtTarget()
  {
    var spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
    Vector3 velocityDirection = target.position - transform.position;
    velocityDirection.Normalize();

    Vector3 bulletSpawnPosition = transform.position + velocityDirection * 0.5f;

    // Try to lead the target via it's velocity?
    Vector3 targetVelocity = target.GetComponent<AIPath>().velocity;
    targetVelocity.Normalize();

    // Note: this is super half-assed and cheesy. To do it for real, 
    // we should also take into account the distance from target.... but this is good enough
    velocityDirection = (velocityDirection + targetVelocity * 0.2f);

    spawnedBullet.transform.position = bulletSpawnPosition;
    spawnedBullet.GetComponent<Rigidbody>().velocity = velocityDirection * bulletSpeed;
  }

  // Update is called once per frame
  void Update()
  {

  }
}
