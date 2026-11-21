using UnityEngine;

[CreateAssetMenu(fileName = "PartG_", menuName = "Scriptable Objects/Genetics/Part Gene")]
public class SO_PartGene : SO_Gene
{
    [SerializeField, Tooltip("Index determines dominance, the lower the index, the higher the dominance.\nIndex 2 is more dominant than index 5.")]
    private SO_PartAllele[] _alleles;

    public override int FindIndex(SO_Allele allele)
        => FindIndex(allele, _alleles);

    public SO_PartAllele GetRandomAllele()
        => _alleles[Random.Range(0, _alleles.Length)];
}
