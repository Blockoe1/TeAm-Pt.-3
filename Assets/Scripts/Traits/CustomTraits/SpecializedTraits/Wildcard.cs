using System;
using UnityEngine;
using static UnityEngine.CullingGroup;

[System.Serializable]
public class Wildcard : Trait, ITriggeredTrait<StartRaceTrigger>
{
    [SerializeField] private Vector2Int randomizedRange;
    public void TriggerTrait(StartRaceTrigger trigger, DeerController deer, DeerData data, bool isSelf)
    {
        string notifyString = "";
        foreach(StatType type in Enum.GetValues(typeof(StatType)))
        {
            if (type != StatType.Speed)
            {
                notifyString += ", ";
            }

            int statChange = UnityEngine.Random.Range(randomizedRange.x, randomizedRange.y);
            data.ModifyStat(type, statChange);
            notifyString += $"{(statChange < 0 ? "" : '+')}{statChange} {type.ToString()}";
        }
        NotificationLog.QueueNotification(notifyString, data);
    }
}
