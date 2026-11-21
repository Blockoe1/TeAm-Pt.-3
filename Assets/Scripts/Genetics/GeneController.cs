using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ColorRegionHandler))]
public class GeneController : MonoBehaviour
{
    //COLOR
    [SerializeField]
    private Dictionary<SO_ColorGene, ColorGenotype> _colorGenotype;

    private ColorRegionHandler colorRegHan;

    //STAT
    [SerializeField]
    private Dictionary<SO_Gene, StatGenotype> _statGenotype;


    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        colorRegHan = GetComponent<ColorRegionHandler>();
        VisualizeColorGenotype();
    }

    [Button]
    private void VisualizeColorGenotype()
    {
        foreach (KeyValuePair<SO_ColorGene, ColorGenotype> kvp in _colorGenotype)
            colorRegHan.VisualizeColor(kvp.Key.MatColorID, GetDominantAllele(kvp).Color);
    }

    //Returns the dominant allele.
    private SO_ColorAllele GetDominantAllele(KeyValuePair<SO_ColorGene, ColorGenotype> kvp)
        => (kvp.Key.FindIndex(kvp.Value.AlleleA) < kvp.Key.FindIndex(kvp.Value.AlleleB)) ? kvp.Value.AlleleA : kvp.Value.AlleleB;
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
}

[Serializable]
public class StatGenotype
{

}