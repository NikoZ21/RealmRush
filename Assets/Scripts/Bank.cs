using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    #region VariablesAndProperties
    [SerializeField] int startingBalance = 150;
    [SerializeField] int currentBalance;
    public int CurrentBalance { get => currentBalance; }
    [SerializeField] TextMeshProUGUI displayBalanceText;
    #endregion 


    private void Awake()
    {
        currentBalance = startingBalance;
        displayBalanceText.text = currentBalance.ToString();
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        displayBalanceText.text = currentBalance.ToString();
    }

    public void WithDraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        displayBalanceText.text = currentBalance.ToString();
        if (currentBalance < 0)
        {
            RestartScene();
        }
    }

    private static void RestartScene()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
