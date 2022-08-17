using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissileBehavior : MonoBehaviour
{

    private GameObject _target;
    private float _missileSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _target = NearestTarget();
    }

    private void Update()
    {
        if (_target != null)
        {
            HomingMissileMovement();
        }
        else
        {
            _target = NearestTarget();
        }
    }

    private GameObject NearestTarget()
    {
        float closestDistance = Mathf.Infinity;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        foreach (var enemy in enemies)
        {
            float distance = Vector3.Magnitude(enemy.transform.position - transform.position);
            if (distance <= closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }

    private void HomingMissileMovement()
    {
        Vector3 diff = _target.transform.position - transform.position;
        diff.Normalize();
        float rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ - 90);
        transform.Translate(Vector3.up * _missileSpeed * Time.deltaTime);
    }
}
