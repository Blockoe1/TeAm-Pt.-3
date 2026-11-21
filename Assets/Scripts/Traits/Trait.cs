using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public abstract class Trait
{
    [SerializeField] private string _name;
    [SerializeField, TextArea] private string _description;

    public string Name => _name;
    public string Description => _description;


}
