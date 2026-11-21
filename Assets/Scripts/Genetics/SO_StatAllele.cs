using UnityEngine;

[CreateAssetMenu(fileName = "StatA_", menuName = "Scriptable Objects/Genetics/StatAllele")]
public class SO_StatAllele : SO_Allele
{
    [SerializeField]
    private RacerStats _stats;

    #region
    public RacerStats Stats { get => _stats; }
    #endregion
}
