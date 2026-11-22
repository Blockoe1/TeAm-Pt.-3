using UnityEngine;

public class BreedTrigger : TraitTrigger
{
    public DeerData Child { get; private set; }
    public StatType[] InheritedStats { get; private set; }

    public BreedTrigger(DeerData child, StatType[] inheritedStats)
    {
        Child = child;
        InheritedStats = inheritedStats;
    }
}
