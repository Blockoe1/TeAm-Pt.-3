using System;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class RandomBetBoost : Trait, ITriggeredTrait<StartRaceTrigger>
{
    [SerializeField] private int numStats;
    [SerializeField] private int statChange;

    public void TriggerTrait(StartRaceTrigger trigger, DeerController deer, bool isSelf)
    {
        if (trigger.WasBetOn)
        {
            System.Random rand = new System.Random();
            StatType[] randomTypes = ((StatType[])Enum.GetValues(typeof(StatType))).OrderBy(_ => rand.Next()).ToArray();
            for(int i = 0; i < numStats && i < randomTypes.Length; i++)
            {
                deer.Data.ModifyStat(randomTypes[i], statChange);
            }
        }
    }
}
