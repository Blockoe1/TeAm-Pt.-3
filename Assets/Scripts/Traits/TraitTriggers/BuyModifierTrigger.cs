using UnityEngine;

public class BuyModifierTrigger : TraitTrigger
{
    public float Price { get; set; }

    public BuyModifierTrigger(float price)
    {
        Price = price;
    }
}
