using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    #region VariablesAndProperties
    [SerializeField] int cost = 75;
    #endregion


    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();
        
        if (bank == null) return false;
        
        if (bank.CurrentBalance < cost) return false;

        Instantiate(tower.gameObject, position, Quaternion.identity);
        bank.WithDraw(cost);
        
        return true;
    }
}
