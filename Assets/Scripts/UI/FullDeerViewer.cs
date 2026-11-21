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
}
