using System;
using System.Collections.Generic;
using UnityEngine;

public class RaceSelectionScreen : BuckScreen
{
    [SerializeField] private BettingScreen _bettingScreen;

    protected override void Start()
    {
        base.Start();
        InitializeSelection();
    }

    /// <summary>
    /// Run during post-race set-up and when exiting betting screen
    /// </summary>
    public void InitializeSelection()
    {
        // Fetch stable data
        List<DeerData> bucks = new List<DeerData>();
        for (int i = 0; i < 8; i++)
        {
            bucks.Add(new DeerData());
            bucks[^1].Name = (i + 1).ToString();
        }

        selected = default;
        InitializeWidgets(bucks);
    }

    public override void Continue()
    {
        // TODO: place selected horse in race

        _screen.SetActive(false);

        _bettingScreen.InitializeBetting();
    }

    public override void BuckSelected(DeerData buck, BuckUI ui)
    {
        if (selected != default)
        {
            selected.Item2.Background.color = Color.wheat;
        }

        selected = (buck, ui);
        ui.Background.color = Color.orange;
    }
}