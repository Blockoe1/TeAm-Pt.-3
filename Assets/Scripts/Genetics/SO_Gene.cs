using UnityEngine;

public class SO_Gene : ScriptableObject
{
    [SerializeField]
    private string _name;

    public virtual int FindIndex(SO_Allele allele) { return -1; }

    public int FindIndex(SO_Allele allele, SO_Allele[] alleles)
    {
        for (int i = 0; i < alleles.Length; i++)
        {
            if (alleles[i] == allele)
                return i;
        }

        Debug.LogError($"Allele {allele.name} is not in the {_name} gene.");
        return -1;
    }
}
