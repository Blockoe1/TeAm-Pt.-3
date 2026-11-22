using UnityEngine;

[System.Serializable]
public class TripTimeModifier : Trait, ITriggeredTrait<TripTrigger>
{
    [SerializeField] private float tripTimeMultiplier = 1;
    public void TriggerTrait(TripTrigger trigger, DeerController deer, DeerData data, bool isSelf)
    {
        trigger.TripTime = trigger.TripTime * tripTimeMultiplier;
    }
}
