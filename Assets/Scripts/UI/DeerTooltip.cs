/*****************************************************************************
// File Name : DeerTooltip.cs
// Author : Arcadia Koederitz
// Creation Date : 10/21/2026
// Last Modified : 10/21/2026
//
// Brief Description : Displays a tooltip of a deer when hovering over it.
*****************************************************************************/
using NaughtyAttributes;
using System;
using TMPro;
using UnityEngine;

public class DeerTooltip : DeerViewer
{
    [SerializeField] private int _statDescSpacing;
    [SerializeField] private string[] _statDescriptions;
    [SerializeField, BoxGroup("References")] protected TMP_Text _traitsText;

    public override void SetDeer(DeerData deerData)
    {
        base.SetDeer(deerData);

        // Traits
        string traitsString = "Traits: ";
        for(int i = 0; i < deerData.Traits.Length; i++)
        {
            if (i != 0)
            {
                traitsString += ", ";
            }
            traitsString += deerData.Traits[i].Name;
        }
        _traitsText.text = traitsString;

    }

    public override string ProcessStats(DeerData deerData)
    {
        string statString = "";
        foreach (StatType type in Enum.GetValues(typeof(StatType)))
        {
            statString += type.ToString();
            for(int i = statString.Length; i < _statDescSpacing; i++)
            {
                statString += " ";
            }


            //data.SetStat(type, UnityEngine.Random.Range(STAT_MIN, STAT_MAX));
        }
        return statString;
    }

}
