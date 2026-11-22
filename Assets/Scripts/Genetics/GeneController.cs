/*
 * Marlow Greenan
 * Created: 11/20/2026
 * Last Updated: 11/21/2026 by Marlow Greenan
 * 
 * Controls genotypes and phenotypes.
 */
using MarUtility.ExecutionManagement;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeneController : Manager
{
    //COLOR
    [SerializeField]
    private ColorRegionHandler _colorRegHan;

    public override void Initialize()
    {
        base.Initialize();
    }

    public void InitializeGenotypes(Dictionary<SO_ColorGene, ColorGenotype> colorGenotype)
    {
        VisualizeColorGenotype(colorGenotype);
    }

    public void VisualizeColorGenotype(Dictionary<SO_ColorGene, ColorGenotype> colorGenotype)
    {
        foreach (KeyValuePair<SO_ColorGene, ColorGenotype> kvp in colorGenotype)
            _colorRegHan.VisualizeColor(kvp.Key.MatColorID, GetDominantAllele(kvp.Key, colorGenotype).Color);
    }

    #region Get Allele
    //Returns the dominant allele.
    private SO_ColorAllele GetDominantAllele(SO_ColorGene gene, Dictionary<SO_ColorGene, ColorGenotype> colorGenotype)
        => (gene.FindIndex(colorGenotype[gene].AlleleA) < gene.FindIndex(colorGenotype[gene].AlleleB)) ? colorGenotype[gene].AlleleA : colorGenotype[gene].AlleleB;

    //Returns a random allele.
    public SO_ColorAllele GetRandomAlleleFromGenotype(SO_ColorGene gene, Dictionary<SO_ColorGene, ColorGenotype> colorGenotype)
    {
        int ran = Random.Range(0, 2);
        return (ran == 0) ? colorGenotype[gene].AlleleA : colorGenotype[gene].AlleleB;
    }
    #endregion
}

[Serializable]
public class ColorGenotype
{
    [SerializeField]
    private SO_ColorAllele _alleleA;
    [SerializeField]
    private SO_ColorAllele _alleleB;

    #region GS
    public SO_ColorAllele AlleleA { get => _alleleA; }
    public SO_ColorAllele AlleleB { get => _alleleB; }
    #endregion

    public ColorGenotype(SO_ColorAllele alleleA, SO_ColorAllele alleleB)
    {
        _alleleA = alleleA;
        _alleleB = alleleB;
    }
}