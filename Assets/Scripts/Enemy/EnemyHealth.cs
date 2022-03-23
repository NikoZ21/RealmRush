using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    #region VariablesAndProperties
    [SerializeField] int maxhitpoints = 5;
    int currentHitPoints;

    Enemy enemy;
    #endregion


    void OnEnable()
    {
        currentHitPoints = maxhitpoints;
    }

    private void OnParticleCollision(GameObject other)
    {
        TakeDamage();
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void TakeDamage()
    {
        currentHitPoints--;

        if (currentHitPoints <= 0)
        {
            enemy.RewardGold();

            gameObject.SetActive(false);
        }
    }

}
