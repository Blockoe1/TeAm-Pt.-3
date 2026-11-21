/*****************************************************************************
// File Name : PurchaseTrait.cs
// Author : Arcadia Koederitz
// Creation Date : 10/04/2026
// Last Modified : 10/04/2026
//
// Brief Description : Trait that modifies horse purchase price.
*****************************************************************************/
using AriUtils;
using UnityEngine;

[DropdownGroup("PurchaseTrait")]
public abstract class PurchaseTrait : Trait
{
    public abstract void ModifyPurchasePrice(ref int purchasePrice);
}
