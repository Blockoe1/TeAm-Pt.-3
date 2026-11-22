using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BettingScreen : BuckScreen
{
    [SerializeField] private RaceSelectionScreen _selectionScreen;
    [SerializeField] private TMP_Text _betText;
    [SerializeField] private int _defaultBet;
    [SerializeField] private int _betIncrement;
    private int bet;

    protected override void Start()
    {
        base.Start();
        _screen.SetActive(false);
    }

    /// <summary>
    /// Run when you hit continue on selection screen
    /// </summary>
    public void InitializeBetting()
    {
        // Fetch relevant deer for the race
        List<DeerController> bucks = new List<DeerController>();
        for (int i = 0; i < 5; i++)
        {
            bucks.Add(new DeerController());
            bucks[^1].Data = new DeerData();
            bucks[^1].Data.Name = (i + 1).ToString();
        }

        selected = default;
        InitializeWidgets(bucks);

        bet = _defaultBet;
        _betText.text = FormatDoe(bet);
    }

    public override void BuckSelected(DeerController buck, BuckUI ui)
    {
        if (selected != default)
        {
            selected.Item2.Background.color = Color.wheat;
        }

        selected = (buck, ui);
        ui.Background.color = Color.orange;
    }

    public override void Continue()
    {
        _screen.SetActive(false);

        // TODO: store bet
        RaceManager.DeerBetOn = selected.Item2.transform.GetSiblingIndex() - widgetsToKeep;
        // TODO: start race
        SceneManager.LoadScene("RaceScene");
    }

    public void BetUp()
    {
        bet += _betIncrement;
        _betText.text = FormatDoe(bet);
    }

    public void BetDown()
    {
        if (bet - _betIncrement <= 0) return;
        bet -= _betIncrement;
        _betText.text = FormatDoe(bet);
    }

    public void Back()
    {
        _screen.SetActive(false);

        _selectionScreen.InitializeSelection();
    }
}
