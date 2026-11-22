/*
 * Marlow Greenan
 * Created: 11/20/2026
 * Last Updated: 11/21/2026 by Marlow Greenan
 * 
 * Controls genotypes and phenotypes.
 */
using MarUtility;
using MarUtility.ExecutionManagement;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public void VisualizeGenotypes(Dictionary<SO_PartGene, PartGenome> partGenotype, Dictionary<SO_ColorGene, ColorGenome> colorGenotype)
    {
        VisualizeColorGenotype(colorGenotype);
        VisualizePartGenotype(partGenotype);
    }

    public void VisualizeColorGenotype(Dictionary<SO_ColorGene, ColorGenome> colorGenotype)
    {
        foreach (KeyValuePair<SO_ColorGene, ColorGenome> genome in colorGenotype)
            _colorRegHan.VisualizeColor(genome.Key.MatColorID, GetDominantAllele(genome.Key, colorGenotype).Color);
    }

    public void VisualizePartGenotype(Dictionary<SO_PartGene, PartGenome> partGenotype)
    {
        Debug.Log("Ran");
        foreach (KeyValuePair<SO_PartGene, PartGenome> genome in partGenotype)
        {
            SO_PartAllele dominant = GetDominantAllele(genome.Key, partGenotype);
            foreach (KeyValuePair<string, Sprite> kvp in dominant.Parts)
            {
                Transform conPt = MarData.FindChildWithName(_colorRegHan.transform, kvp.Key);
                if (conPt == null)
                {
                    Debug.LogError($"No child under {_colorRegHan.gameObject.name} has the name {kvp.Key}.");
                    continue;
                }
                
                switch (_colorRegHan.RenderType)
                {
                    case ERendererType.SPRITE: 
                        SpriteRenderer sr = conPt.GetComponent<SpriteRenderer>(); 
                        if (sr == null)
                        {
                            Debug.LogError($"{conPt.gameObject.name} does not have a SpriteRenderer component.");
                            break;
                        }
                        sr.sprite = kvp.Value;
                        sr.material.SetTexture(kvp.Key, kvp.Value.texture);
                        break;
                    case ERendererType.IMAGE:
                        Image i = conPt.GetComponent<Image>();
                        if (i == null)
                        {
                            Debug.LogError($"{conPt.gameObject.name} does not have a Image component.");
                            break;
                        }
                        i.sprite = kvp.Value;
                        i.material.SetTexture("_" + genome.Key.MatTextureID, kvp.Value.texture);
                        i.SetNativeSize();
                        break;
                }
            }
        }
    }

    #region Get Allele
    //Returns the dominant allele.
    private SO_ColorAllele GetDominantAllele(SO_ColorGene gene, Dictionary<SO_ColorGene, ColorGenome> colorGenotype)
        => (gene.FindIndex(colorGenotype[gene].AlleleA) < gene.FindIndex(colorGenotype[gene].AlleleB)) ? colorGenotype[gene].AlleleA : colorGenotype[gene].AlleleB;
    private SO_PartAllele GetDominantAllele(SO_PartGene gene, Dictionary<SO_PartGene, PartGenome> partGenotype)
    => (gene.FindIndex(partGenotype[gene].AlleleA) < gene.FindIndex(partGenotype[gene].AlleleB)) ? partGenotype[gene].AlleleA : partGenotype[gene].AlleleB;

    //Returns a random allele.
    public SO_ColorAllele GetRandomAlleleFromGenotype(SO_ColorGene gene, Dictionary<SO_ColorGene, ColorGenome> colorGenotype)
    {
        int ran = Random.Range(0, 2);
        return (ran == 0) ? colorGenotype[gene].AlleleA : colorGenotype[gene].AlleleB;
    }

    public SO_PartAllele GetRandomAlleleFromGenotype(SO_PartGene gene, Dictionary<SO_PartGene, PartGenome> partGenotype)
    {
        int ran = Random.Range(0, 2);
        return (ran == 0) ? partGenotype[gene].AlleleA : partGenotype[gene].AlleleB;
    }
    #endregion
}

[Serializable]
public class ColorGenome
{
    [SerializeField]
    private SO_ColorAllele _alleleA;
    [SerializeField]
    private SO_ColorAllele _alleleB;

    #region GS
    public SO_ColorAllele AlleleA { get => _alleleA; }
    public SO_ColorAllele AlleleB { get => _alleleB; }
    #endregion

    public ColorGenome(SO_ColorAllele alleleA, SO_ColorAllele alleleB)
    {
        _alleleA = alleleA;
        _alleleB = alleleB;
    }
}

[Serializable]
public class PartGenome
{
    [SerializeField]
    private SO_PartAllele _alleleA;
    [SerializeField]
    private SO_PartAllele _alleleB;

    #region GS
    public SO_PartAllele AlleleA { get => _alleleA; }
    public SO_PartAllele AlleleB { get => _alleleB; }
    #endregion

    public PartGenome(SO_PartAllele alleleA, SO_PartAllele alleleB)
    {
        _alleleA = alleleA;
        _alleleB = alleleB;
    }
}