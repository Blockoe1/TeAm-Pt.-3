/*****************************************************************************
// File Name : MisbehaveTrait.cs
// Author : Arcadia Koederitz
// Creation Date : 10/04/2026
// Last Modified : 10/04/2026
//
// Brief Description : Trait that triggers when a horse misbehaves
*****************************************************************************/
using AriUtils;
using UnityEngine;

[DropdownGroup("MisbehaveTrait")]
public abstract class MisbehaveTrait : Trait
{
    public abstract void OnMisbehave();
}
