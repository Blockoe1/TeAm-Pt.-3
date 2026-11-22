/*****************************************************************************
// File Name : TraitBase.cs
// Author : Arcadia Koederitz
// Creation Date : 10/04/2026
// Last Modified : 10/04/2026
//
// Brief Description : Base script for custom deer traits.
*****************************************************************************/
using System;
using UnityEngine;

[System.Serializable]
public abstract class Trait
{
    [SerializeField] private string _name;
    [SerializeField, TextArea] private string _description;
    [SerializeField] private int _priority;
    [SerializeField] private bool _randomlySelectable = true;
    [SerializeField] private TraitAlignment _traitAlignment;
    [SerializeField] private TriggerFlags _triggerFlags = TriggerFlags.Race;

    public int TraitIndex { get; set; }

    public string Name => _name;
    public string Description => _description;
    public int Priority => _priority;
    public bool RandomlySelectable => _randomlySelectable;
    public TraitAlignment Alignment => _traitAlignment;
    public TriggerFlags TrigFlags => _triggerFlags;

    public enum TraitAlignment
    { 
        Good,
        Bad,
        Special
    }

    [Flags]
    public enum TriggerFlags
    { 
        None = 0,
        Race = 1 << 0,
        Owned = 1 << 1,
        Global = 1 << 2
    }

    // Specialized trait functions for traits that modify specific info on this deer only.
    public virtual int GetInheritedTraitIndex(TraitManager tm)
    {
        return TraitIndex;
    }

    public virtual int ModifyStat(StatType stat, int currentValue)
    {
        return currentValue;
    }

    public virtual float ModifySellPrice(float currentPrice)
    {
        return currentPrice;
    }
}
