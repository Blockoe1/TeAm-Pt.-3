/*****************************************************************************
// File Name : ObtainTrait.cs
// Author : Arcadia Koederitz
// Creation Date : 10/04/2026
// Last Modified : 10/04/2026
//
// Brief Description : Trait that triggers when a deer is obtained.
*****************************************************************************/
using AriUtils;
using UnityEngine;

[DropdownGroup("ObtainTrait")]
public abstract class ObtainTrait : Trait
{
    public abstract void OnObtain();
}
