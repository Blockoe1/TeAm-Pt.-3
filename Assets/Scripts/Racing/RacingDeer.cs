using System;
using System.Collections;
using UnityEngine;

public class RacingDeer : MonoBehaviour
{
    [SerializeField] private DeerData data;

    /// <summary>
    /// Will start when the deer gets started
    /// </summary>
    private void Start()
    {
        RaceManager.Broadcast_StartRace += StartRace;
    }

    private void StartRace()
    {
        
    }


    private void OnDestroy()
    {
        RaceManager.Broadcast_StartRace -= StartRace;
    }
}
