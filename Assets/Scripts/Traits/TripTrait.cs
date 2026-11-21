/*****************************************************************************
// File Name : TripTrait.cs
// Author : Arcadia Koederitz
// Creation Date : 10/04/2026
// Last Modified : 10/04/2026
//
// Brief Description : Trait that triggers when a deer trips.
*****************************************************************************/
using AriUtils;
using UnityEngine;

[DropdownGroup("TripTrait")]
public abstract class TripTrait : Trait
{
    public abstract void OnTrip();
}
