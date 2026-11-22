using AriUtils;
using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField,ComponentReference] private TMP_Text moneyText;
    private void OnEnable()
    {
        MoneyManager.OnBalanceChanged += Handle_OnBalanceChanged;
    }

    private void Start()
    {
        Handle_OnBalanceChanged(MoneyManager.Instance.TotalCents);
    }

    private void OnDisable()
    {
        MoneyManager.OnBalanceChanged -= Handle_OnBalanceChanged;
    }

    private void Handle_OnBalanceChanged(int balance)
    {
        moneyText.text = MoneyManager.FormatCents(balance);
    }
}
