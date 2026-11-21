/*****************************************************************************
// File Name : DeerTooltip.cs
// Author : Arcadia Koederitz
// Creation Date : 10/21/2026
// Last Modified : 10/21/2026
//
// Brief Description : Displays a tooltip of a deer when hovering over it.
*****************************************************************************/
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class DeerTooltip : DeerViewer
{
    [SerializeField, BoxGroup("References")] protected TMP_Text _traitsText;
}
