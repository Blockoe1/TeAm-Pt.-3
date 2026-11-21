/*****************************************************************************
// File Name : TraitManager.cs
// Author : Arcadia Koederitz
// Creation Date : 10/04/2026
// Last Modified : 10/04/2026
//
// Brief Description : Handles ordering traits and calling traits.
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TraitManager : MonoBehaviour
{
    private Dictionary<Type, int> eventTypeLookup = new();
    private List<TraitBase>[] ActiveTraits;

    public void Initialize()
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

    }

    public void RemoveTrait<T>(Trait<T> trait) where T : TraitTrigger
    {
        List<TraitBase> typedTraits = GetTraitList(typeof(T));
        typedTraits.Remove(trait);
    }

    public void TraitEvent<T>(T eventData) where T : TraitTrigger // TODO: Needs to take in the 
    {
        List<TraitBase> typedTraits = GetTraitList(typeof(T));
        foreach(TraitBase t in typedTraits)
        {
            if(t is Trait<T>)
            {
                
            }
        }
    }

    private List<TraitBase> GetTraitList(Type type)
    {
        int index = eventTypeLookup[type];
        return ActiveTraits[index];
    }
}
