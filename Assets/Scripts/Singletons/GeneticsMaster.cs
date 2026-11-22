/*
 * Marlow Greenan
 * Created: 11/20/2026
 * Last Updated: 11/21/2026 by Marlow Greenan
 * 
 * Holds data for genetics.
 */
using MarUtility.ExecutionManagement;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GeneticsMaster : Manager
{
    private static GeneticsMaster inst;

    [SerializeField]
    private GameObject _racerPrefab;

    [SerializeField]
    private SO_ColorGene[] _colorGenes;

    [SerializeField]
    private SO_PartGene[] _partGenes;
 
    #region GS
    public static GeneticsMaster INST { get => inst; }
    public SO_ColorGene[] ColorGenes { get => _colorGenes; }
    public SO_PartGene[] PartGenes { get => _partGenes; }
    #endregion

    public override void Initialize()
    {
        //Set instance.
        if (inst == null)
            inst = this;
        else
        {
            Debug.LogError("Multiple copies of GeneticsMaster exists, please delete one.");
            Destroy(this);
        }

        base.Initialize();
    }

    #region Breeding
    //Spawns an offspring with the combined genotypes of parentA and parentB.
    public DeerController Breed(DeerController parentA, DeerController parentB)
    {
        GameObject osGO = Instantiate(_racerPrefab, PlayerManager.Instance.transform);
        DeerController osDeerCon = osGO.GetComponent<DeerController>();
        GeneController osGenCon = osGO.GetComponent<GeneController>();

        if (osGenCon == null)
        {
            Debug.LogError("Racer prefab does not have a GeneController.");
            return null;
        }

        osDeerCon.Data = BreedStats(parentA.Data, parentB.Data);
        osDeerCon.Data.ColorGenotype = CombineColorGenotype(parentA, parentB);
        osDeerCon.Data.PartGenotype = CombinePartGenotype(parentA, parentB);
        osDeerCon.Data.Name = DeerNames.GetRandomName();

        // Inherit Traits
        osDeerCon.Data.Traits = BreedTraits(parentA.Data, parentB.Data);

        osGenCon.Initialize();
        osGenCon.VisualizeGenotypes(osDeerCon.Data.PartGenotype, osDeerCon.Data.ColorGenotype);//VisualizeColorGenotype(osDeerCon.Data.ColorGenotype);

        return osDeerCon;
    }

    private Trait[] BreedTraits(DeerData parentA, DeerData parentB)
    {
        Trait[] traits = new Trait[DeerData.BASE_TRAIT_NUM];
        for(int i = 0; i < traits.Length; i++)
        {
            // Assumes that only 2 traits are used.
            DeerData inheritedParent = i % 2 == 0 ? parentA : parentB;
            int index = Random.Range(0, inheritedParent.Traits.Length);
            traits[i] = TraitManager.TraitDatabase[inheritedParent.Traits[index].GetInheritedTraitIndex()];
        }
        return traits;
    }

    //Combines the stats of the parents with the breeding stat formula.
    private DeerData BreedStats(DeerData parentA, DeerData parentB)
    {
        DeerData combinedData = new DeerData();

        List<StatType> pool = new List<StatType>{ StatType.Acceleration, StatType.Balance, StatType.Speed, StatType.Stamina, StatType.Discipline, StatType.Charisma };

        //Set stats received from parentA.
        for (int i = 0; i < 3; i++)
        {
            int ran = Random.Range(0, pool.Count);
            combinedData.SetStat(pool[ran], CalculateBredStat(pool[ran], parentA));
            pool.RemoveAt(ran);
        }

        //Set stats received from parentB.
        for (int i = 0; i < pool.Count; i++)
            combinedData.SetStat(pool[i], CalculateBredStat(pool[i], parentB));

        return combinedData;
    }

    //Calculates a possible bred stat for a deer.
    private int CalculateBredStat(StatType stat, DeerData parent)
        => parent.GetStat(stat) + Random.Range(-2, 3);

    //Combines parentA and parentB color genotypes.
    private Dictionary<SO_ColorGene, ColorGenome> CombineColorGenotype(DeerController parentA, DeerController parentB)
    {
        if (parentA.Data.ColorGenotype.Count != parentB.Data.ColorGenotype.Count)
        {
            Debug.LogError($"{parentA.Data.Name} does not have the same amount of genes in the color genotype as {parentB.Data.Name}.\nCheck for missing genotypes.");
            return null;
        }

        Dictionary<SO_ColorGene, ColorGenome> combinedGenotype = new Dictionary<SO_ColorGene, ColorGenome>();

        foreach (KeyValuePair<SO_ColorGene, ColorGenome> kvp in parentA.Data.ColorGenotype)
            combinedGenotype.TryAdd(kvp.Key, CombineColorGenome(parentA, parentB, kvp.Key));

        return combinedGenotype;
    }
    //Creates a genotype with 1 random allele from both parents.
    private ColorGenome CombineColorGenome(DeerController parentA, DeerController parentB, SO_ColorGene gene)
        => new ColorGenome(parentA.GeneCon.GetRandomAlleleFromGenotype(gene, parentA.Data.ColorGenotype), parentB.GeneCon.GetRandomAlleleFromGenotype(gene, parentB.Data.ColorGenotype));


    private Dictionary<SO_PartGene, PartGenome> CombinePartGenotype(DeerController parentA, DeerController parentB)
    {
        if (parentA.Data.PartGenotype.Count != parentB.Data.PartGenotype.Count)
        {
            Debug.LogError($"{parentA.Data.Name} does not have the same amount of genes in the color genotype as {parentB.Data.Name}.\nCheck for missing genotypes.");
            return null;
        }

        Dictionary<SO_PartGene, PartGenome> combinedGenotype = new Dictionary<SO_PartGene, PartGenome>();

        foreach (KeyValuePair<SO_PartGene, PartGenome> kvp in parentA.Data.PartGenotype)
            combinedGenotype.TryAdd(kvp.Key, CombinePartGenome(parentA, parentB, kvp.Key));

        return combinedGenotype;
    }
    //Creates a genotype with 1 random allele from both parents.
    private PartGenome CombinePartGenome(DeerController parentA, DeerController parentB, SO_PartGene gene)
        => new PartGenome(parentA.GeneCon.GetRandomAlleleFromGenotype(gene, parentA.Data.PartGenotype), parentB.GeneCon.GetRandomAlleleFromGenotype(gene, parentB.Data.PartGenotype));

    #endregion

}
