/*****************************************************************************
// File Name : ITraitTrigger.cs
// Author : Arcadia Koederitz
// Creation Date : 10/04/2026
// Last Modified : 10/04/2026
//
// Brief Description : Adds generic support for trait triggers
*****************************************************************************/
public interface ITriggeredTrait<T> where T: TraitTrigger
{
    public abstract void TriggerTrait(T trait, DeerController deer, bool isSelf);
}
