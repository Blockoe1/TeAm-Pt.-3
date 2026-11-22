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
using UnityEngine.UI;

public abstract class DeerViewer : MonoBehaviour
{
    [SerializeField, BoxGroup("References")] protected TMP_Text _nameText;
    [SerializeField, BoxGroup("References")] protected TMP_Text _winLossText;
    [SerializeField, BoxGroup("References")] protected TMP_Text _statsText;
    [SerializeField, BoxGroup("References")] protected TMP_Text _costText;
    [SerializeField, BoxGroup("References")] protected RawImage _deerView;

    private DeerData loadedData;

    public virtual void SetDeer(DeerData deerData, CostParams cost = CostParams.None)
    {
        loadedData = deerData;

        _nameText.text = "<b>" + deerData.Name + "<b>";
        _winLossText.text = "<u>W/L:</u> " + deerData.Wins.ToString() + " / " + deerData.Races.ToString();
        _statsText.text = ProcessStats(deerData);
        if (_deerView != null) _deerView.texture = deerData.Controller.Rt;

        switch (cost)
        { 
            case CostParams.None:
                if (_costText != null) _costText.gameObject.SetActive(false);
                break;
            case CostParams.Sell:
                _costText.gameObject.SetActive(true);
                _costText.text = "<u>Sell Price:</u> " + MoneyManager.FormatCents(deerData.CalculateSellPrice());
                break;
            case CostParams.Buy:
                _costText.gameObject.SetActive(true);
                _costText.text = "<u>Buy Price:</u> " + MoneyManager.FormatCents(deerData.CalculatePrice());
                break;
            case CostParams.Bet:
                _costText.gameObject.SetActive(true);
                _costText.text = "Betting Odds: " + ProcessBetMult(deerData) + ":1";
                break;
        }
    }

    public string ProcessBetMult(DeerData deerData)
    {
        string multString = "";
        multString += deerData.BetMultiplier;
        return multString;
    }

    public abstract string ProcessStats(DeerData deerData);
}
public enum CostParams
{ 
    None,
    Buy,
    Sell,
    Bet
}

