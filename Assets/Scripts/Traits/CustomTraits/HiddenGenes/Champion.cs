using AriUtils;
using UnityEngine;

[DropdownGroup("Hidden Genes")]
[System.Serializable]
public class Champion : Trait
{
    [SerializeField] private int allStatsBoost = 4;
    public override int ModifyStat(StatType stat, int currentValue)
    {
        return currentValue + allStatsBoost;
    }
}
