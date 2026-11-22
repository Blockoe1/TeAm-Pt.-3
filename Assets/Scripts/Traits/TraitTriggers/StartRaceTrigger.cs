/*****************************************************************************
// File Name : StartRaceTrait.cs
// Author : LFehlberg
// Creation Date : 10/04/2026
// Last Modified : 10/04/2026
//
// Brief Description : Trait that triggers when a deer starts a race
*****************************************************************************/
using AriUtils;
using UnityEngine;

public class StartRaceTrigger : TraitTrigger
{
    public bool WasBetOn {  get; private set; }

    public StartRaceTrigger(bool wasBetOn)
    {
        WasBetOn = wasBetOn;
    }
}
