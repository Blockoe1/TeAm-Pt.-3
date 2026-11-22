using System;
using UnityEngine;

public class Wildcard : Trait, ITriggeredTrait<StartRaceTrigger>
{
    [SerializeField] private Vector2Int randomizedRange;
    public void TriggerTrait(StartRaceTrigger trigger, DeerController deer, DeerData data, bool isSelf)
    {
        foreach(StatType type in Enum.GetValues(typeof(StatType)))
        {
            deer.ApplyStatModifier(type, UnityEngine.Random.Range(randomizedRange.x, randomizedRange.y));
        }
    }
}
