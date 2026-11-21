using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private float money;
    private float currentLoan;
    private float interestRate = 0.10f;
    private float loanDuration = 3f;
    private float loanTimer;

    public void ModifyMoney(float amount)
    {
        money += amount;
    }

    public void TakeLoan(float amount)
    {
        currentLoan += amount;
        money += amount;
        loanTimer = loanDuration;
    }

    public void RepayLoan()
    {
        if (currentLoan > money)
        {
            Debug.Log("Not enough money to repay the loan.");
            return;
        }
        money -= currentLoan;
        currentLoan = 0;
    }

    public void applyInterest()
    {
        if (currentLoan > 0)
        {
            currentLoan += currentLoan * interestRate;
        }
        loanTimer -= 1f;
    }
}
