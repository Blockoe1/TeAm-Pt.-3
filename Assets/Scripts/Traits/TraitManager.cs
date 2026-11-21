/*****************************************************************************
// File Name : TraitManager.cs
// Author : Arcadia Koederitz
// Creation Date : 10/04/2026
// Last Modified : 10/04/2026
//
// Brief Description : Handles ordering traits and calling traits.
*****************************************************************************/
using AriUtils;
using MarUtility.ExecutionManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class TraitManager : Manager
{
    private Dictionary<Type, int> eventTypeLookup = new();
    private List<Trait>[] ActiveTraits;

    private static TraitManager instance;

    public override void Initialize()
    {
        SingletonUtil.SetSingleton(ref instance, this);
        // Setup the event trigger trait indicies.
        Type[] triggerTypes = Assembly.GetAssembly(typeof(TraitTrigger)).GetTypes().Where(x => !x.IsAbstract && !x.IsInterface).ToArray();
        ActiveTraits = new List<Trait>[triggerTypes.Length];
        for(int i = 0; i < triggerTypes.Length; i++)
        {
            eventTypeLookup.Add(triggerTypes[i], i);
        }
    }

    public static void AddTrait(Trait trait)
    {
        if (instance == null) { Debug.LogError("No TraitManager exists."); return; }
        List<Trait> typedTraits = instance.GetTraitList(trait.GetType());
        typedTraits.Add(trait);
        typedTraits = typedTraits.OrderBy(x => x.Priority).ToList();
    }

    public static void RemoveTrait(Trait trait)
    {
        if (instance == null) { Debug.LogError("No TraitManager exists."); return; }
        List<Trait> typedTraits = instance.GetTraitList(trait.GetType());
        typedTraits.Remove(trait);
        typedTraits = typedTraits.OrderBy(x => x.Priority).ToList();
    }

    /// <summary>
    /// Notifies all deer traits that an event has occured.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="eventData"></param>
    /// <param name="triggeredDeer"></param>
    public static void TraitEvent<T>(T eventData, DeerController triggeredDeer) where T : TraitTrigger // TODO: Needs to take in the relevant DeerController.
    {
        if (instance == null) { Debug.LogError("No TraitManager exists."); return; }
        List<Trait> typedTraits = instance.GetTraitList(typeof(T));
        foreach(Trait tb in typedTraits)
        {
            //if (!tb.IsGlobal && tb.AppliedDeer != triggeredDeer) { continue; }
            if(tb is ITraitTrigger<T> t)
            {
                t.TriggerTrait(eventData, triggeredDeer);
            }
        }
    }

    private List<Trait> GetTraitList(Type type)
    {
        int index = eventTypeLookup[type];
        return ActiveTraits[index];
    }
}
