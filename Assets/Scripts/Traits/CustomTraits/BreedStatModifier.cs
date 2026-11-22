using System;
using UnityEngine;

[System.Serializable]
public class BreedStatModifier : Trait, ITriggeredTrait<BreedTrigger>
{
    [SerializeField] private int statModifier;

    public void TriggerTrait(BreedTrigger trigger, DeerController deer, DeerData data, bool isSelf)
    {
        foreach(StatType stat in trigger.InheritedStats)
        {
            trigger.Child.ModifyStat(stat, statModifier);
        }
    }
}
