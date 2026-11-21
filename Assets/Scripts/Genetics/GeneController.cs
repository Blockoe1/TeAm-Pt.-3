/*
 * Marlow Greenan
 * Created: 11/20/2026
 * Last Updated: 11/21/2026 by Marlow Greenan
 * 
 * Controls genotypes and phenotypes.
 */
using MarUtility.ExecutionManagement;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ColorRegionHandler))]
public class GeneController : Manager
{
    //COLOR
    [SerializeField]
    private Dictionary<SO_ColorGene, ColorGenotype> _colorGenotype;

    private ColorRegionHandler colorRegHan;

    #region GS
    public Dictionary<SO_ColorGene, ColorGenotype> ColorGenotype { get => _colorGenotype; set => _colorGenotype = value; }

    #endregion

    public override void Initialize()
    {
        base.Initialize();

        colorRegHan = GetComponent<ColorRegionHandler>();

        InitializeGenotypes();

    }

    public void InitializeGenotypes()
    {
        VisualizeColorGenotype();
    }

    //Randomizes all genotypes.
    [Button]
    public void RandomizeGenotype()
    {
        RandmizeColorGenotype();
    }

    //Randomize color genotype.
    private void RandmizeColorGenotype()
    {
        Dictionary<SO_ColorGene, ColorGenotype> newColorGenotype = new Dictionary<SO_ColorGene, ColorGenotype>();
        foreach (KeyValuePair<SO_ColorGene, ColorGenotype> kvp in _colorGenotype)
            newColorGenotype.TryAdd(kvp.Key, new ColorGenotype(kvp.Key.GetRandomAllele(), kvp.Key.GetRandomAllele()));
        _colorGenotype = newColorGenotype;
        VisualizeColorGenotype();
    }

    [Button]
    private void VisualizeColorGenotype()
    {
        foreach (KeyValuePair<SO_ColorGene, ColorGenotype> kvp in _colorGenotype)
            colorRegHan.VisualizeColor(kvp.Key.MatColorID, GetDominantAllele(kvp.Key).Color);
    }

    #region Get Allele
    //Returns the dominant allele.
    private SO_ColorAllele GetDominantAllele(SO_ColorGene gene)
        => (gene.FindIndex(_colorGenotype[gene].AlleleA) < gene.FindIndex(_colorGenotype[gene].AlleleB)) ? _colorGenotype[gene].AlleleA : _colorGenotype[gene].AlleleB;

    //Returns a random allele.
    public SO_ColorAllele GetRandomAlleleFromGenotype(SO_ColorGene gene)
    {
        int ran = Random.Range(0, 2);
        return (ran == 0) ? _colorGenotype[gene].AlleleA : _colorGenotype[gene].AlleleB;
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