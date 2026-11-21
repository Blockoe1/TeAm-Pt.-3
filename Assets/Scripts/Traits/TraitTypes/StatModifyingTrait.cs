/*****************************************************************************
// File Name : StatModifyingTrait.cs
// Author : Arcadia Koederitz
// Creation Date : 10/04/2026
// Last Modified : 10/04/2026
//
// Brief Description : Trait that modifies the actual used value of a stat.
*****************************************************************************/

public abstract class StatModifyingTrait : TraitTrigger
{
    public StatType Stat { get; private set; }
    public int Value { get; set; }
}
