using UnityEngine;

[CreateAssetMenu(fileName = "GeneC_", menuName = "Scriptable Objects/Genetics/Color Gene")]
public class SO_ColorGene : SO_Gene
{
    [SerializeField, Tooltip("Index determines dominance, the lower the index, the higher the dominance.\nIndex 2 is more dominant than index 5.")]
    private SO_ColorAllele[] _alleles;

    [SerializeField]
    private string _matColorID;

    public override int FindIndex(SO_Allele allele)
        => FindIndex(allele, _alleles);

    public SO_ColorAllele GetRandomAllele()
        => _alleles[Random.Range(0, _alleles.Length)];
    #region GS
    public string MatColorID { get => _matColorID; }
    #endregion
}
