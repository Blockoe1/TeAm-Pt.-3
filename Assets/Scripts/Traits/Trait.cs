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

    public virtual int ProcessStat(StatType stat, int value)
    {
        return value;
    }
}
