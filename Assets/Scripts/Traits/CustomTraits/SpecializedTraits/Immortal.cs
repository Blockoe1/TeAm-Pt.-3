using UnityEngine;

[System.Serializable]
public class Immortal : Trait, ITriggeredTrait<AgeTrigger>
{
    [SerializeField] private int minAge;
    public void TriggerTrait(AgeTrigger trigger, DeerController deer, DeerData data, bool isSelf)
    {
        if (data.Age > minAge)
        {
            trigger.Age = data.Age;
        }
    }
}
