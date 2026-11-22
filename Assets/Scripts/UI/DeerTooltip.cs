/*****************************************************************************
// File Name : DeerTooltip.cs
// Author : Arcadia Koederitz
// Creation Date : 10/21/2026
// Last Modified : 10/21/2026
//
// Brief Description : Displays a tooltip of a deer when hovering over it.
*****************************************************************************/
using AriUtils;
using NaughtyAttributes;
using System;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class DeerTooltip : DeerViewer
{
    private static DeerTooltip instance;

    [SerializeField] private int _statDescSpacing;
    [SerializeField] private string[] _statDescriptions;
    [SerializeField, BoxGroup("References")] protected TMP_Text _traitsText;
    [SerializeField, ComponentReference] private CanvasGroup cg;

    public static DeerTooltip Instance => instance;

    private void Awake()
    {
        SingletonUtil.SetSingleton(ref instance, this);
    }

    public override void SetDeer(DeerData deerData, CostParams costParams)
    {
        if (deerData == null)
        {
            cg.alpha = 0;
            return;
        }
        else
        {
            cg.alpha = 1;
        }
        base.SetDeer(deerData, costParams);

        // Traits
        string traitsString = "Traits: ";
        for(int i = 0; i < deerData.Traits.Length; i++)
        {
            if (deerData.Traits[i] == null) { continue; }
            if (i != 0)
            {
                traitsString += ", ";
            }
            traitsString += deerData.Traits[i].Name;
        }
        _traitsText.text = traitsString;

    }

    public void MoveTo(Vector2 screenPosition)
    {
        transform.position = screenPosition;
    }

    public override string ProcessStats(DeerData deerData)
    {
        string statString = "";
        foreach (StatType type in Enum.GetValues(typeof(StatType)))
        {
            statString += type.ToString() + "\t\t" + GetStatDescription(deerData.ApplyTraits(type, deerData.GetStat(type))) + "\n";
        }

        return statString.Substring(0, statString.Length - 1); // Remove last newline.
    }


    private string GetStatDescription(int statValue)
    {
        int index = (statValue - 1) / (DeerData.STAT_MAX / _statDescriptions.Length);
        return _statDescriptions[Mathf.Clamp(index, 0, _statDescriptions.Length - 1)];
    }
}
