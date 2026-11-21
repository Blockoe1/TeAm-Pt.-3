/*****************************************************************************
// File Name : FullDeerViewer.cs
// Author : Arcadia Koederitz
// Creation Date : 10/21/2026
// Last Modified : 10/21/2026
//
// Brief Description : Displays all stats related to a deer in detail.
*****************************************************************************/
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class FullDeerViewer : DeerViewer
{
    [SerializeField, BoxGroup("References")] protected TMP_Text _ageText;
    [SerializeField, BoxGroup("References")] protected TMP_Text[] _traitNamesText;
    [SerializeField, BoxGroup("References")] protected TMP_Text[] _traitDescriptionsText;

    public override void SetDeer(DeerData deerData)
    {
        base.SetDeer(deerData);
        _ageText.text = "Age: " + deerData.Age.ToString();

        // Process Traits
        for(int i = 0; i < deerData.Traits.Length; i++)
        {
            if (_traitNamesText.Length > i)
            {
                _traitNamesText[i].text = deerData.Traits[i].Name;
            }
            if (_traitDescriptionsText.Length > i)
            {
                _traitDescriptionsText[i].text = deerData.Traits[i].Description;
            }
        }
    }

    public override string ProcessStats(DeerData deerData)
    {
        throw new System.NotImplementedException();
    }
}
