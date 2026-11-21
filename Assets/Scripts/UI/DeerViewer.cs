/*****************************************************************************
// File Name : DeerViewer.cs
// Author : Arcadia Koederitz
// Creation Date : 10/21/2026
// Last Modified : 10/21/2026
//
// Brief Description : Base class for UI elements that display deer stats.
*****************************************************************************/
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public abstract class DeerViewer : MonoBehaviour
{
    [SerializeField, BoxGroup("References")] protected TMP_Text _nameText;
    [SerializeField, BoxGroup("References")] protected TMP_Text _winLossText;
    [SerializeField, BoxGroup("References")] protected TMP_Text _statsText;

    private DeerData loadedData;

    public virtual void SetDeer(DeerData deerData)
    {
        loadedData = deerData;

        _nameText.text = deerData.Name;
        _winLossText.text = deerData.Wins.ToString() + " / " + deerData.Races.ToString();
        _statsText.text = ProcessStats(deerData);
    }

    public abstract string ProcessStats(DeerData deerData);
}
