using UnityEngine;

[System.Serializable]
public class TestTrait : Trait, ITriggeredTrait<TripTrigger>
{
    public void TriggerTrait(TripTrigger trait, DeerController deer, DeerData data, bool isSelf)
    {
        Debug.Log("Trait called for " + deer.name);
    }

    public override int ModifyStat(StatType stat, int currentValue)
    {
        if (stat == StatType.Acceleration)
        {
            return currentValue + 5;
        }
        return currentValue;
    }
}
