/*****************************************************************************
// File Name : PurchaseTrait.cs
// Author : Arcadia Koederitz
// Creation Date : 10/04/2026
// Last Modified : 10/04/2026
//
// Brief Description : Trait that modifies horse purchase price.
*****************************************************************************/
public abstract class PurchaseTrait : TraitTrigger
{
    public int PurchasePrice { get; set; }
}
