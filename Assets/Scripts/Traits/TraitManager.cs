/*****************************************************************************
// File Name : TraitManager.cs
// Author : Arcadia Koederitz
// Creation Date : 10/04/2026
// Last Modified : 10/04/2026
//
// Brief Description : Handles ordering traits and calling traits.
*****************************************************************************/
using MarUtility.ExecutionManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class TraitManager : Manager
{
    private Dictionary<Type, int> eventTypeLookup = new();
    private List<TraitBase>[] ActiveTraits;

    public override void Initialize()
    {
        // Setup the event trigger trait indicies.
        Type[] triggerTypes = Assembly.GetAssembly(typeof(TraitTrigger)).GetTypes().Where(x => !x.IsAbstract && !x.IsInterface).ToArray();
        ActiveTraits = new List<TraitBase>[triggerTypes.Length];
        for(int i = 0; i < triggerTypes.Length; i++)
        {
            eventTypeLookup.Add(triggerTypes[i], i);
        }
    }

    public void AddTrait<T>(Trait<T> trait) where T : TraitTrigger
    {
        List<TraitBase> typedTraits = GetTraitList(typeof(T));
        typedTraits.Add(trait);
        typedTraits = typedTraits.OrderBy(x => x.Priority).ToList();
    }

    public void RemoveTrait<T>(Trait<T> trait) where T : TraitTrigger
    {
        List<TraitBase> typedTraits = GetTraitList(typeof(T));
        typedTraits.Remove(trait);
        typedTraits = typedTraits.OrderBy(x => x.Priority).ToList();
    }

    /// <summary>
    /// Notifies all deer traits that an event has occured.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="eventData"></param>
    /// <param name="triggeredDeer"></param>
    public void TraitEvent<T>(T eventData, DeerController triggeredDeer) where T : TraitTrigger // TODO: Needs to take in the relevant DeerController.
    {
        List<TraitBase> typedTraits = GetTraitList(typeof(T));
        foreach(TraitBase tb in typedTraits)
        {
            if (!tb.IsGlobal && tb.AppliedDeer != triggeredDeer) { continue; }
            if(tb is Trait<T> t)
            {
                t.TriggerTrait(eventData, triggeredDeer);
            }
        }
    }

    private List<TraitBase> GetTraitList(Type type)
    {
        int index = eventTypeLookup[type];
        return ActiveTraits[index];
    }
}
