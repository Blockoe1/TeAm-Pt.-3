/*****************************************************************************
// File Name : MoneyManager.cs
// Author : Alex Lindsey
// Creation Date : 11/020/2026
// Last Modified : 11/021/2026
//
// Brief Description : Handles the player's money, loans, and interest rates.
*****************************************************************************/
using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    [SerializeField] private int startingMoney = 100;
    [SerializeField] private int totalCents = 0;

    public int TotalCents => totalCents;

    public event Action<int> OnBalanceChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        totalCents = startingMoney;
    }

    public void ModifyMoney(int amount)
    {
        totalCents += amount;
        OnBalanceChanged?.Invoke(totalCents);
    }

    public bool CanAfford(int amount)
    {
        return totalCents >= amount;
    }

    public bool SpendMoney(int amount)
    {
        if (!CanAfford(amount))
            return false;

        totalCents -= amount;
        OnBalanceChanged?.Invoke(totalCents);
        return true;
    }

    public void EarnMoney(int amount)
    {
        totalCents += amount;
        OnBalanceChanged?.Invoke(totalCents);
    }

    public string GetFormattedBalance()
    {
        return FormatCents(totalCents);
    }

    public static string FormatCents(int cents)
    {
        int dollars = cents / 100;
        long remainingCents = Math.Abs(cents % 100);
        return $"${dollars:N0}.{remainingCents:D2}";
    }
}
