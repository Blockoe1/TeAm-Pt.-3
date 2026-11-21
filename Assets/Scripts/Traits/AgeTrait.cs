/*****************************************************************************
// File Name : AgeTrait.cs
// Author : Arcadia Koederitz
// Creation Date : 10/04/2026
// Last Modified : 10/04/2026
//
// Brief Description : Trait that triggers when a deer ages.
*****************************************************************************/
using AriUtils;
using UnityEngine;

[DropdownGroup("AgeTrait")]
public abstract class AgeTrait : Trait
{
    public abstract void OnAge(ref int age);
}
