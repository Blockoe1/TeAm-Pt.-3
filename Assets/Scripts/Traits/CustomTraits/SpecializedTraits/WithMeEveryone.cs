using UnityEngine;

[System.Serializable]
public class WithMeEveryone : Trait, ITriggeredTrait<TripTrigger>
{
    public void TriggerTrait(TripTrigger trigger, DeerController deer, DeerData data, bool isSelf)
    {
        foreach(GameObject goInRace in RaceManager.Instance.GameObjectsInRace)
        {
            if (goInRace != deer.gameObject && goInRace.TryGetComponent(out RacingDeer racingDeer))
            {
                racingDeer.CurrentState = RacingDeer.STATES.TRIPPED;
            }
        }
    }
}
