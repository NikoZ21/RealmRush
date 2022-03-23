using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region VariablesAndProperties
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int poolSize = 5;
    [SerializeField] float spawnTimer = 1f;
    
    GameObject[] pool;
    #endregion

    private void OnValidate()
    {
        spawnTimer = Mathf.Clamp(spawnTimer, 0, Mathf.Infinity);
        poolSize = Mathf.Clamp(poolSize, 0, 1000);
    }
    void Awake()
    {
        PopulatePool();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    private void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }

    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTimer);
            EnableObjectInPool();
        }
    }

    private void EnableObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }
}
