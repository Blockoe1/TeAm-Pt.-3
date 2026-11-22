using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AlleleP_", menuName = "Scriptable Objects/Genetics/Part Allele")]
public class SO_PartAllele : SO_Allele
{
    [SerializeField]
    private Dictionary<string, Sprite> _parts;

    #region
    public Dictionary<string, Sprite> Parts { get => _parts; set => _parts = value; }
    #endregion
}
