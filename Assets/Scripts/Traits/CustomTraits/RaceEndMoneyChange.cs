using UnityEngine;

[System.Serializable]
public class RaceEndMoneyChange : Trait, ITriggeredTrait<EndRaceTrigger>
{
    private enum Requirements
    {
        None,
        Winner,
        Loser
    }

    [SerializeField] private int moneyChange;
    [SerializeField] private Requirements requirements;

    public void TriggerTrait(EndRaceTrigger trigger, DeerController deer, DeerData data, bool isSelf)
    {
        if (requirements == Requirements.None
            || (requirements == Requirements.Winner && deer == trigger.Winner)
            || (requirements == Requirements.Loser && deer != trigger.Winner))
        {
            MoneyManager.Instance.ModifyMoney(moneyChange);
            NotificationLog.QueueNotification($"{(moneyChange < 0 ? "" : '+')}{MoneyManager.FormatCents(moneyChange)}", data);
        }
    }
}
