using UnityEngine;

public class SO_Allele : ScriptableObject
{
    [SerializeField]
    protected string _name;

    #region GS
    private string Name { get => _name; }
    #endregion
}
