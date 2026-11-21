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

public class TraitManager : Manager
{
    [SerializeReference, ClassDropdown] private Trait[] _traitDatabase;


    public override void Initialize()
    {
        for(int i = 0; i < _traitDatabase.Length; i++)
        {
            _traitDatabase[i].TraitIndex = i;
        }
    }

    public Trait GetTrait(int index)
    {
        return _traitDatabase[index];
    }

    public int GetTraitIndex<T>()
    {
        return Array.FindIndex(_traitDatabase, x => x.GetType() == typeof(T));
    }

    /// <summary>
    /// Notifies all relevant deer that an event for a given trait has been triggered.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="eventData"></param>
    /// <param name="triggeredDeer"></param>
    public static void TraitEvent<T>(T eventData, DeerController triggeredDeer) where T : TraitTrigger // TODO: Needs to take in the relevant DeerController.
    {
        // Loop through deer in race.

        // Loop through deer owned.
    }
}
