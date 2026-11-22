/*****************************************************************************
// File Name : TraitDatabase.cs
// Author : Arcadia Koederitz
// Creation Date : 10/21/2026
// Last Modified : 10/21/2026
//
// Brief Description : Handles modifying design data of traits.
*****************************************************************************/
using AriUtils;
using MarUtility.ExecutionManagement;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class TraitManager : Manager
{
    [Header("Trait Database")]
    [SerializeReference, ClassDropdown] private Trait[] _traitDatabase;

    private static TraitManager instance;

    public static ReadOnlyArray<Trait> TraitDatabase => instance == null ? null : instance._traitDatabase;

    public override void Initialize()
    {
        SingletonUtil.SetSingleton(ref instance, this);
        for(int i = 0; i < _traitDatabase.Length; i++)
        {
            _traitDatabase[i].TraitIndex = i;
        }
    }

    public static int GetTraitIndex<T>()
    {
        if (instance == null) { return -1; }
        return Array.FindIndex(instance._traitDatabase, x => x.GetType() == typeof(T));
    }

    public static Trait GetRandomTrait()
    {
        Trait[] filteredTraits = instance._traitDatabase.Where(x => x.RandomlySelectable).ToArray();
        if (filteredTraits.Length == 0)
        {
            return null;
        }
        return filteredTraits[UnityEngine.Random.Range(0, filteredTraits.Length)];
    }

    public static Trait GetRandomTrait(Trait.TraitAlignment alignment)
    {
        Trait[] filteredTraits = instance._traitDatabase.Where(x => x.RandomlySelectable && x.Alignment == alignment).ToArray();
        if (filteredTraits.Length == 0)
        {
            return null;
        }
        return filteredTraits[UnityEngine.Random.Range(0, filteredTraits.Length)];
    }

    /// <summary>
    /// Notifies all relevant deer that an event for a given trait has been triggered.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="eventData"></param>
    /// <param name="triggeredDeer"></param>
    public static void TraitEvent<T>(T eventData, DeerController triggeredDeer) where T : TraitTrigger
    {
        // Loop through deer in current race.
        if (RaceManager.DeerInRace != null)
        {
            foreach (DeerData deer in RaceManager.DeerInRace)
            {
                deer.TriggerTraits(eventData, triggeredDeer, triggeredDeer.Data, deer, Trait.TriggerFlags.Race);
            }
        }

        // Loop through deer owned.
        foreach (DeerData deer in PlayerManager.Instance.OwnedDeer.Keys)
        {
            deer.TriggerTraits(eventData, triggeredDeer, triggeredDeer.Data, deer, Trait.TriggerFlags.Owned);
        }
    }

    /// <summary>
    /// Notifies all relevant deer that an event for a given trait has been triggered.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="eventData"></param>
    /// <param name="triggeredDeer"></param>
    public static void TraitEvent<T>(T eventData, DeerData triggeredDeer) where T : TraitTrigger
    {
        // Loop through deer in current race.
        if (RaceManager.DeerInRace != null)
        {
            foreach (DeerData deer in RaceManager.DeerInRace)
            {
                deer.TriggerTraits(eventData, null, triggeredDeer, deer, Trait.TriggerFlags.Race);
            }
        }

        // Loop through deer owned.
        foreach (DeerData deer in PlayerManager.Instance.OwnedDeer.Keys)
        {
            deer.TriggerTraits(eventData, null, triggeredDeer, deer, Trait.TriggerFlags.Owned);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe event references.
    }
}
