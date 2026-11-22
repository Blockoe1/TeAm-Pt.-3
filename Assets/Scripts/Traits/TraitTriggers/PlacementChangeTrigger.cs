using UnityEngine;

public class PlacementChangeTrigger : TraitTrigger
{
    public int Placement { get; private set; }
    
    public PlacementChangeTrigger(int Placement)
    {
        this.Placement = Placement;
    }
}
