using System;
using UnityEngine;

[System.Serializable]
public class RaceEndStatChange : Trait, ITriggeredTrait<EndRaceTrigger>
{
    private enum Requirements
    { 
        None,
        Winner,
        Loser
    }

    [SerializeField] private StatType stat;
    [SerializeField] private int statChange;
    [SerializeField] private Requirements requirements;
    public void TriggerTrait(EndRaceTrigger trigger, DeerController deer, DeerData data, bool isSelf)
    {
        if (requirements == Requirements.None
            || (requirements == Requirements.Winner && deer == trigger.Winner)
            || (requirements == Requirements.Loser && deer != trigger.Winner))
        {
            data.ModifyStat(stat, statChange);
            NotificationLog.QueueNotification($"{(statChange < 0 ? '-' : '+')}{statChange} {stat.ToString()}", data);
        }
    }
}
