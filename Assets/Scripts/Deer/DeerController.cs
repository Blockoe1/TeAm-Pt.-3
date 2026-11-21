/*
 * Marlow Greenan
 * Created: 11/20/2026
 * Last Updated: 11/20/2026 by Marlow Greenan
 * 
 * Holds stats for a racer.
 */
using MarUtility.ExecutionManagement;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(GeneController))]
public class DeerController : Manager
{
    [SerializeField] private DeerData data;

    private GeneController geneCon;
    private ColorRegionHandler colorRegHan;

    [SerializeField, BoxGroup("Simulation")]
    private DeerController _simBreedingPartner;

    #region GS
    public GeneController GeneCon { get => geneCon; }
    public DeerData Data { get => data; set => data = value; }
    #endregion

    public override void Initialize()
    {
        base.Initialize();

        geneCon = GetComponent<GeneController>();
        geneCon.VisualizeColorGenotype(data.ColorGenotype);
    }

    #region Inspector
    [Button]
    private void Breed()
        => GeneticsMaster.INST.Breed(this, _simBreedingPartner);

    //Randomizes all genotypes.
    [Button]
    public void Randomize()
    {
        data = DeerData.NewRandomDeer();
        geneCon.VisualizeColorGenotype(data.ColorGenotype);
    }
    #endregion
}
