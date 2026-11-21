using UnityEngine;

[System.Serializable]
public class TestTrait : Trait, ITraitTrigger<TripTrait>
{
    public void TriggerTrait(TripTrait trait, DeerController deer)
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
