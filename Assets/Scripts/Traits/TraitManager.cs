/*****************************************************************************
// File Name : TraitDatabase.cs
// Author : Arcadia Koederitz
// Creation Date : 10/04/2026
// Last Modified : 10/04/2026
//
// Brief Description : Handles modifying design data of traits.
*****************************************************************************/
using AriUtils;
using MarUtility.ExecutionManagement;
using System;
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

        // Subscribe to relevant events.
    }

    public static int GetTraitIndex<T>()
    {
        if (instance == null) { return -1; }
        return Array.FindIndex(instance._traitDatabase, x => x.GetType() == typeof(T));
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
        foreach (DeerData deer in RaceManager.DeerInRace)
        {
            // Trigger each trait in those deer.
            foreach (Trait trait in deer.Traits)
            {
                if (trait is ITraitTrigger<T> triggered
                    && trait.TrigFlags.HasFlag(Trait.TriggerFlags.Race)
                    && (trait.TrigFlags.HasFlag(Trait.TriggerFlags.Global) || triggeredDeer.Data == deer)) // Global traits trigger even when the event was not on this deer.
                {
                    triggered.TriggerTrait(eventData, triggeredDeer);
                }
            }
        }

        // Loop through deer owned.
        foreach (DeerData deer in PlayerManager.Instance.OwnedDeer)
        {
            // Trigger each trait in those deer.
            foreach(Trait trait in deer.Traits)
            {
                if (trait is ITraitTrigger<T> triggered
                    && trait.TrigFlags.HasFlag(Trait.TriggerFlags.Owned)
                    && (trait.TrigFlags.HasFlag(Trait.TriggerFlags.Global) || triggeredDeer.Data == deer))
                {
                    triggered.TriggerTrait(eventData, triggeredDeer);
                }
            }
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe event references.
    }
}
