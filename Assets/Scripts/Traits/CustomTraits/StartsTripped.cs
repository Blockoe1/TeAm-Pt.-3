using UnityEngine;

[System.Serializable]
public class StartsTripped : Trait, ITriggeredTrait<StartRaceTrigger>
{
    [SerializeField] private bool ignoreSelf;
    public void TriggerTrait(StartRaceTrigger trigger, DeerController deer, DeerData data, bool isSelf)
    {
        if (!(ignoreSelf && isSelf))
        {
            if (deer.TryGetComponent(out RacingDeer racer))
            {
                racer.CurrentState = RacingDeer.STATES.TRIPPED;
            }
        }
    }
}
