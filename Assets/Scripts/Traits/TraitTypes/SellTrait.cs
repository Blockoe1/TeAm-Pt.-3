/*****************************************************************************
// File Name : SellTrait.cs
// Author : Arcadia Koederitz
// Creation Date : 10/04/2026
// Last Modified : 10/04/2026
//
// Brief Description : Trait that modifies sell price of horses.
*****************************************************************************/
using AriUtils;
using UnityEngine;

[DropdownGroup("SellTrait")]
public abstract class SellTrait : Trait
{
    public abstract void ModifySellPrice(ref int sellPrice);
}
