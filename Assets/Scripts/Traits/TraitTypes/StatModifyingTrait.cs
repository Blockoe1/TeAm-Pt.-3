/*****************************************************************************
// File Name : StatModifyingTrait.cs
// Author : Arcadia Koederitz
// Creation Date : 10/04/2026
// Last Modified : 10/04/2026
//
// Brief Description : Trait that modifies the actual used value of a stat.
*****************************************************************************/
using AriUtils;
using UnityEngine;

[DropdownGroup("StatModifyingTrait")]
public abstract class StatModifyingTrait : Trait
{
    public abstract int GetModifiedStat(StatType stat, int baseStat);
}
