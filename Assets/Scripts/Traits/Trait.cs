/*****************************************************************************
// File Name : TraitBase.cs
// Author : Arcadia Koederitz
// Creation Date : 10/04/2026
// Last Modified : 10/04/2026
//
// Brief Description : Base script for custom deer traits.
*****************************************************************************/
using UnityEngine;

[System.Serializable]
public abstract class Trait
{
    [SerializeField] private string _name;
    [SerializeField, TextArea] private string _description;
    [SerializeField] private int _priority;
    [SerializeField] private bool _isGlobal;

    public string Name => _name;
    public string Description => _description;
    public int Priority => _priority;
    public bool IsGlobal => _isGlobal;

    
}
