using System;
using UnityEngine;

public class OneTimeWonder : Trait, ITriggeredTrait<EndRaceTrigger>
{
    [SerializeField] private int boost = 5;
    [SerializeField] private int statDrop = -3;
    public void TriggerTrait(EndRaceTrigger trigger, DeerController deer, DeerData data, bool isSelf)
    {
        foreach(StatType type in Enum.GetValues(typeof(StatType)))
        {
            data.ModifyStat(type, statDrop);
        }
    }

    public override int ModifyStat(StatType stat, int currentValue)
    {
        return currentValue + boost;
    }
}
