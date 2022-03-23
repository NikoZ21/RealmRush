using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    #region VariablesAndProperties
    [SerializeField] int cost = 75;
    public int Cost { get => cost; }
    [SerializeField] float buildSpeed = 0.5f;
    #endregion

    private void Start()
    {
        StartCoroutine(BuildTimer());
    }

    private IEnumerator BuildTimer()
    {

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in transform)
        {
            yield return new WaitForSeconds(buildSpeed);
            child.gameObject.SetActive(true);
        }

    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank == null) return false;

        if (bank.CurrentBalance >= cost)
        {
            Instantiate(tower.gameObject, position, Quaternion.identity);
            bank.WithDraw(cost);
            return true;
        }
        return false;
    }
}
