using UnityEngine;

public class EndRaceTrigger : TraitTrigger
{
    public DeerController Winner { get; private set; }

    public EndRaceTrigger(DeerController controller)
    {
        Winner = controller;
    }
}
