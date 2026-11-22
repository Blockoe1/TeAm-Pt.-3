/*
 * Marlow Greenan
 * Created: 11/20/2026
 * Last Updated: 11/20/2026 by Marlow Greenan
 * 
 * Holds stats for a racer.
 */
using MarUtility.ExecutionManagement;
using NaughtyAttributes;
using System;
using UnityEngine;

[RequireComponent(typeof(GeneController))]
public class DeerController : Manager
{
    [SerializeField] private DeerData data;
    private RenderTexture rt;

    private int[] statModifiers;

    private GeneController geneCon;
    private ColorRegionHandler colorRegHan;

    [SerializeField]
    private DeerAnimator anim;

    [SerializeField, BoxGroup("Simulation")]
    private DeerController _simBreedingPartner;

    #region GS
    public GeneController GeneCon { get => geneCon; }
    public DeerData Data { get => data; set => data = value; }
    public RenderTexture Rt { get => rt; set => rt = value; }
    public DeerAnimator Anim { get => anim; set => anim = value; }
    #endregion

    public override void Initialize()
    {
        base.Initialize();

        geneCon = GetComponent<GeneController>();
        geneCon.VisualizeGenotypes(data);//VisualizeColorGenotype(data.ColorGenotype);

        statModifiers = new int[Enum.GetValues(typeof(StatType)).Length];
    }

    #region Stats
    public void ApplyStatModifier(StatType stat, int modifier)
    {
        statModifiers[(int)stat] += modifier;
    }

    public void ResetStatModifiers()
    {
        for(int i = 0; i < statModifiers.Length; i++)
        {
            statModifiers[i] = 0;
        }
    }

    public int GetStat(StatType stat)
    {
        int value = data.GetStat(stat);
        // Apply temporary stat modifiers.
        value += statModifiers[(int)stat];
        // Apply apply traits from the data.
        value = data.ApplyTraits(stat, value);

        return value;
    }
    #endregion

    #region Inspector
    [Button]
    private void Breed()
        => GeneticsMaster.INST.Breed(this, _simBreedingPartner);

    //Randomizes all genotypes.
    [Button]
    public void Randomize()
    {
        data = DeerData.NewRandomDeer();
        geneCon.VisualizeGenotypes(data);//.VisualizeColorGenotype(data.ColorGenotype);
    }
    #endregion
}
