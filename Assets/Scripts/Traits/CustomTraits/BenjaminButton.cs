using UnityEngine;

[System.Serializable]
public class BenjaminButton : Trait, ITriggeredTrait<BornTrigger>, ITriggeredTrait<AgeTrigger>
{
    [SerializeField] private int startingAge = 10;
    public void TriggerTrait(BornTrigger trigger, DeerController deer, DeerData data, bool isSelf)
    {
        if (isSelf)
        {
            data.Age = startingAge;
        }
    }

    public void TriggerTrait(AgeTrigger trigger, DeerController deer, DeerData data, bool isSelf)
    {
        if (isSelf)
        {
            int ageDifference = trigger.Age - data.Age;
            trigger.Age = data.Age - ageDifference;
        }
    }
}
