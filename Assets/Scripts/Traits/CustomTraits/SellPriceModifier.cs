using UnityEngine;

[System.Serializable]
public class SellPriceModifier : Trait
{
    [SerializeField] private float sellModifier;
    public override float ModifySellPrice(float currentPrice)
    {
        return currentPrice * (1 + sellModifier);
    }
}
