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

    [SerializeField, BoxGroup("Simulation")]
    private DeerController _simBreedingPartner;

    #region GS
    public GeneController GeneCon { get => geneCon; }
    public DeerData Data { get => data; set => data = value; }
    #endregion

    public override void Initialize()
    {
        geneCon = GetComponent<GeneController>();

        base.Initialize();
    }

    #region Inspector
    [Button]
    private void Breed()
        => GeneticsMaster.INST.Breed(this, _simBreedingPartner);
    #endregion
}
