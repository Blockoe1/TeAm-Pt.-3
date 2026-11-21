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

public abstract class SellTrait : TraitTrigger
{
    public int SellPrice { get; set; }
}
