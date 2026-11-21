/*****************************************************************************
// File Name : PlacementChangeTrait.cs
// Author : Arcadia Koederitz
// Creation Date : 10/04/2026
// Last Modified : 10/04/2026
//
// Brief Description : Trait that triggers when a deer's placement in a race changes.
*****************************************************************************/
using AriUtils;
using UnityEngine;

[DropdownGroup("PlacementChangeTrait")]
public abstract class PlacementChangeTrait : Trait
{
    public abstract void OnPlacementChange();
}
