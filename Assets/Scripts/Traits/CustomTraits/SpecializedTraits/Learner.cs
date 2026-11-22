using System;
using UnityEngine;

[System.Serializable]
public class Learner : Trait, ITriggeredTrait<EndRaceTrigger>
{
    [SerializeField] private int statBoost;
    public void TriggerTrait(EndRaceTrigger trigger, DeerController deer, DeerData data, bool isSelf)
    {
        if (trigger.Winner != deer)
        {
            StatType lowestStatType = StatType.Speed; // Changes speed by default.
            int lowestStatValue = DeerData.STAT_MAX;

            foreach (StatType statType in Enum.GetValues(typeof(StatType)))
            {
                if (data.GetStat(statType) <= lowestStatValue)
                {
                    lowestStatType = statType;
                    lowestStatValue = data.GetStat(statType);
                }
            }

            data.ModifyStat(lowestStatType, statBoost);
        }
    }
}
