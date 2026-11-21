/*****************************************************************************
// File Name : RaceEndTrait.cs
// Author : Arcadia Koederitz
// Creation Date : 10/04/2026
// Last Modified : 10/04/2026
//
// Brief Description : Trait that triggers when a race ends.
*****************************************************************************/
using AriUtils;
using UnityEngine;

[DropdownGroup("RaceEndTrait")]
public abstract class RaceEndTrait : Trait
{
    public abstract void OnRaceEnd();
}
