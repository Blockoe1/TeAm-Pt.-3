/*
 * Marlow Greenan
 * Created: 11/20/2026
 * Last Updated: 11/20/2026 by Marlow Greenan
 * 
 * Holds stats for a racer.
 */
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(GeneController))]
public class Racer : MonoBehaviour
{
    [SerializeField] private DeerData data;


    [SerializeField, ReadOnly]
    private RacerStats _geneticStats;

    private GeneController geneCon;

    private void Initialize()
    {
        geneCon = GetComponent<GeneController>();
    }

    private void CalculateGeneStats()
    {

    }
}
