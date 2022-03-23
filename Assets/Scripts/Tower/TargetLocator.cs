using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    #region VariablesAndProperties
    [SerializeField] ParticleSystem projectileParticles;
    [SerializeField] float range = 15;
    [SerializeField] Transform weapon;
    [SerializeField] Transform target;
    #endregion


    void Start()
    {
        target = FindObjectOfType<EnemyMover>().transform;
    }

    void Update()
    {
        FindClosestTarget();
        AimWeapn();
    }

    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        Transform closestTarget = null;
        if (closestTarget == null)
            Debug.Log("it is empty");

        float maxDistance = Mathf.Infinity;


        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                if (closestTarget != null)
                    Debug.Log("not null");

                maxDistance = targetDistance;
            }
        }
        target = closestTarget;
    }

    private void AimWeapn()
    {
        float targetDistance = Vector3.Distance(transform.position, target.transform.position);

        weapon.LookAt(target);

        Attack(targetDistance < range);
    }

    void Attack(bool isActive)
    {
        var emmisionModule = projectileParticles.emission;
        emmisionModule.enabled = isActive;
    }
}
