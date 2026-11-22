using UnityEngine;

[System.Serializable]
public class PurchaseModifierTrait : Trait, ITriggeredTrait<BuyModifierTrigger>
{
    [SerializeField] private float moneyModifier;
    // PurchaseModifier does not give access to DeerController.
    public void TriggerTrait(BuyModifierTrigger trigger, DeerController deer, DeerData data, bool isSelf)
    {
        trigger.Price += trigger.Price * moneyModifier;
    }
}
