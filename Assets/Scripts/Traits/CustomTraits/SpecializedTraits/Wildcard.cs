using System;
using UnityEngine;

[System.Serializable]
public class Wildcard : Trait, ITriggeredTrait<StartRaceTrigger>
{
    [SerializeField] private Vector2Int randomizedRange;
    public void TriggerTrait(StartRaceTrigger trigger, DeerController deer, DeerData data, bool isSelf)
    {
        foreach(StatType type in Enum.GetValues(typeof(StatType)))
        {
            data.ModifyStat(type, UnityEngine.Random.Range(randomizedRange.x, randomizedRange.y));
        }
    }
}
