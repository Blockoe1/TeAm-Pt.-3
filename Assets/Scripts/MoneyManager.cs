using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    [Header("Starting Values")]
    [SerializeField] private float startingMoney = 1f;

    [Header("Loan Settings")]
    [SerializeField] private float interestRate = 0.10f;
    [SerializeField] private int loanDurationTurns = 3;

    private float money;
    private float currentLoan;
    private int loanTurnsRemaining;

    public float Money => money;
    public float CurrentLoan => currentLoan;
    public int LoanTurnsRemaining => loanTurnsRemaining;

    public event Action OnLoanDue;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        money = startingMoney;
    }

    public void ModifyMoney(float amount)
    {
        money += amount;
    }

    public bool CanAfford(float amount)
    {
        return money >= amount;
    }

    public bool SpendMoney(float amount)
    {
        if (!CanAfford(amount))
            return false;

        money -= amount;
        return true;
    }

    public void EarnMoney(float amount)
    {
        money += amount;
    }

    public void TakeLoan(float amount)
    {
        if (amount <= 0f)
        {
            Debug.LogWarning("Cannot take a loan of zero or less.");
            return;
        }

        currentLoan += amount;
        money += amount;

        loanTurnsRemaining = loanDurationTurns;
    }

    public void AdvanceLoanTurn()
    {
        if (currentLoan <= 0f)
            return;

        currentLoan += currentLoan * interestRate;

        loanTurnsRemaining--;

        if (loanTurnsRemaining <= 0)
        {
            OnLoanDue?.Invoke();
        }
    }

    public bool RepayLoan()
    {
        if (currentLoan <= 0f)
            return true;

        if (money < currentLoan)
            return false;

        money -= currentLoan;
        currentLoan = 0f;
        loanTurnsRemaining = 0;

        return true;
    }

    public void ClearLoan()
    {
        currentLoan = 0f;
        loanTurnsRemaining = 0;
    }
}
