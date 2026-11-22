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
    [SerializeField, BoxGroup("References")] protected TMP_Text _costText;

    private DeerData loadedData;

    public virtual void SetDeer(DeerData deerData, CostParams cost = CostParams.None)
    {
        loadedData = deerData;

        _nameText.text = deerData.Name;
        _winLossText.text = deerData.Wins.ToString() + " / " + deerData.Races.ToString();
        _statsText.text = ProcessStats(deerData);

        switch(cost)
        { 
            case CostParams.None:
                _costText.gameObject.SetActive(false);
                break;
            case CostParams.Sell:
                _costText.gameObject.SetActive(true);
                _costText.text = "Sell Price: " + MoneyManager.FormatCents(deerData.CalculateSellPrice());
                break;
            case CostParams.Buy:
                _costText.gameObject.SetActive(true);
                _costText.text = "Buy Price: " + MoneyManager.FormatCents(deerData.CalculatePrice());
                break;
        }
    }

    public abstract string ProcessStats(DeerData deerData);
}
public enum CostParams
{ 
    None,
    Buy,
    Sell
}
