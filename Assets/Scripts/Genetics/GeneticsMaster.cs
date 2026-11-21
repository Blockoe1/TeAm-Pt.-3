/*
 * Marlow Greenan
 * Created: 11/20/2026
 * Last Updated: 11/20/2026 by Marlow Greenan
 * 
 * Holds data for genetics.
 */
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class GeneticsMaster : MonoBehaviour
{
    private static GeneticsMaster inst;

    [SerializeField]
    private GameObject _racerPrefab;

    [SerializeField, BoxGroup("Simulation")]
    private GameObject _simRacerA;
    [SerializeField, BoxGroup("Simulation")]
    private GameObject _simRacerB;

    #region GS
    public static GeneticsMaster INST { get => inst; }
    #endregion

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        //Set instance.
        if (inst == null)
            inst = this;
        else
        {
            Debug.LogError("Multiple copies of ColorMaster exists, please delete one.");
            Destroy(this);
        }
    }

    //Spawns an offspring with the combined genotypes of parentA and parentB.
    public void Breed(GeneController parentA, GeneController parentB)
    {
        GameObject osGO = Instantiate(_racerPrefab);
        GeneController osGenCon = osGO.GetComponent<GeneController>();

        if (osGenCon == null)
        {
            Debug.LogError("Racer prefab does not have a GeneController.");
            return;
        }

        osGenCon.ColorGenotype = CombineColorGenotypes(parentA, parentB);

        osGenCon.Initialize();
    }

    //Combines parentA and parentB color genotypes.
    private Dictionary<SO_ColorGene, ColorGenotype> CombineColorGenotypes(GeneController parentA, GeneController parentB)
    {
        if (parentA.ColorGenotype.Count != parentB.ColorGenotype.Count)
        {
            Debug.LogError($"{parentA.gameObject.name} does not have the same amount of genes in the color genotype as {parentB}.\nCheck for missing genotypes.");
            return null;
        }

        Dictionary<SO_ColorGene, ColorGenotype> combinedGenotype = new Dictionary<SO_ColorGene, ColorGenotype>();

        foreach (KeyValuePair<SO_ColorGene, ColorGenotype> kvp in parentA.ColorGenotype)
            combinedGenotype.TryAdd(kvp.Key, CombineColorGenotype(parentA, parentB, kvp.Key));

        return combinedGenotype;
    }

    //Creates a genotype with 1 random allele from both parents.
    private ColorGenotype CombineColorGenotype(GeneController parentA, GeneController parentB, SO_ColorGene gene)
        => new ColorGenotype(parentA.GetRandomAllele(gene), parentB.GetRandomAllele(gene));

    #region Inspector
    [Button]
    private void Breed()
        => Breed(_simRacerA.GetComponent<GeneController>(), _simRacerB.GetComponent<GeneController>());
    #endregion
}
