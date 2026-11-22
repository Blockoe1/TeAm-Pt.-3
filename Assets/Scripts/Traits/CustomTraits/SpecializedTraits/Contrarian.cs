using UnityEngine;

[System.Serializable]
public class Contrarian : Trait
{
    public override int ModifyStat(StatType stat, int currentValue)
    {
        return Mathf.Clamp(DeerData.STAT_MAX + 1 - currentValue, DeerData.STAT_MIN, DeerData.STAT_MAX + 10);
    }
}
