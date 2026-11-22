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
    public void Breed(DeerController parentA, DeerController parentB)
    {
        GameObject osGO = Instantiate(_racerPrefab, parentA.transform.parent);
        DeerController osDeerCon = osGO.GetComponent<DeerController>();
        GeneController osGenCon = osGO.GetComponent<GeneController>();

        if (osGenCon == null)
        {
            Debug.LogError("Racer prefab does not have a GeneController.");
            return;
        }

        osDeerCon.Data = BreedStats(parentA.Data, parentB.Data);
        osDeerCon.Data.ColorGenotype = CombineColorGenotypes(parentA, parentB);

        osGenCon.Initialize();
        osGenCon.VisualizeColorGenotype(osDeerCon.Data.ColorGenotype);
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
        => parent.GetStat(stat) + Random.Range(-3, 4);

    //Combines parentA and parentB color genotypes.
    private Dictionary<SO_ColorGene, ColorGenome> CombineColorGenotypes(DeerController parentA, DeerController parentB)
    {
        if (parentA.Data.ColorGenotype.Count != parentB.Data.ColorGenotype.Count)
        {
            Debug.LogError($"{parentA.Data.Name} does not have the same amount of genes in the color genotype as {parentB.Data.Name}.\nCheck for missing genotypes.");
            return null;
        }

        Dictionary<SO_ColorGene, ColorGenome> combinedGenotype = new Dictionary<SO_ColorGene, ColorGenome>();

        foreach (KeyValuePair<SO_ColorGene, ColorGenome> kvp in parentA.Data.ColorGenotype)
            combinedGenotype.TryAdd(kvp.Key, CombineColorGenotype(parentA, parentB, kvp.Key));

        return combinedGenotype;
    }

    //Creates a genotype with 1 random allele from both parents.
    private ColorGenome CombineColorGenotype(DeerController parentA, DeerController parentB, SO_ColorGene gene)
        => new ColorGenome(parentA.GeneCon.GetRandomAlleleFromGenotype(gene, parentA.Data.ColorGenotype), parentB.GeneCon.GetRandomAlleleFromGenotype(gene, parentB.Data.ColorGenotype));
    #endregion

}
