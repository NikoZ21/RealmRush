using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region VariablesAndProperties
    [SerializeField] int goldReward = 25;
    [SerializeField] int goldSteal = 25;

    Bank bank;
    #endregion


    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        if (bank == null) return;
        bank.Deposit(goldReward);
    }

    public void StealGold()
    {
        if (bank == null) return;
        bank.WithDraw(goldSteal);

    }
}
