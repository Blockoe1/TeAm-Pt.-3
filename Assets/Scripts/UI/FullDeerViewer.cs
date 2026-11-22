/*****************************************************************************
// File Name : FullDeerViewer.cs
// Author : Arcadia Koederitz
// Creation Date : 10/21/2026
// Last Modified : 10/21/2026
//
// Brief Description : Displays all stats related to a deer in detail.
*****************************************************************************/
using NaughtyAttributes;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FullDeerViewer : DeerViewer
{
    [SerializeField, BoxGroup("References")] protected TMP_Text _ageText;
    [SerializeField, BoxGroup("References")] protected TMP_Text[] _traitNamesText;
    [SerializeField, BoxGroup("References")] protected TMP_Text[] _traitDescriptionsText;
    [SerializeField] private Color boostedStatColor;
    [SerializeField] private Color nerfedStatColor;

    public override void SetDeer(DeerData deerData)
    {
        base.SetDeer(deerData);
        _ageText.text = "Age: " + deerData.Age.ToString();

        // Process Traits
        for (int i = 0; i < deerData.Traits.Length; i++)
        {
            if (deerData.Traits[i] == null) { continue; }
            if (_traitNamesText.Length > i)
            {
                _traitNamesText[i].text = deerData.Traits[i].Name;
            }
            if (_traitDescriptionsText.Length > i)
            {
                _traitDescriptionsText[i].text = deerData.Traits[i].Description;
            }*/
        }
    }

    public override string ProcessStats(DeerData deerData)
    {
        string statString = "";
        foreach (StatType type in Enum.GetValues(typeof(StatType)))
        {
            int baseStat = deerData.GetStat(type);
            statString += type.ToString() + "\t\t" + baseStat;
            int modifiedStat = deerData.ApplyTraits(type, deerData.GetStat(type));
            if (modifiedStat != baseStat)
            {
                string colorString = modifiedStat > baseStat ? boostedStatColor.ToHexString() : nerfedStatColor.ToHexString();
                statString += $"<color=#{colorString}> ({modifiedStat})</color>";
            }

            statString += "\n";
        }

        return statString.Substring(0, statString.Length - 1); // Remove last newline.
    }
}
