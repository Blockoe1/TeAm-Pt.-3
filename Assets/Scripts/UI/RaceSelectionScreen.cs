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
        PlayerManager.OnDeerChanged += Handle_OnDeerChanged;
    }

    private void OnDestroy()
    {
        PlayerManager.OnDeerChanged -= Handle_OnDeerChanged;
    }

    private void Handle_OnDeerChanged(DeerData changedDeer, bool wasAdded)
    {
        InitializeSelection();
    }

    /// <summary>
    /// Run during post-race set-up and when exiting betting screen or deer inventory changing
    /// </summary>
    public void InitializeSelection()
    {
        // Fetch stable data
        List<DeerData> bucks = PlayerManager.Instance.OwnedDeer;

        selected = default;
        InitializeWidgets(bucks);
    }

    public override void Continue()
    {
        // TODO: place selected horse in race

        _screen.SetActive(false);

        _bettingScreen.InitializeBetting(selected.Item1);
    }

    public override void BuckSelected(DeerData buck, BuckUI ui)
    {
        if (selected != default)
        {
            selected.Item2.Background.color = Color.wheat;
        }

        selected = (buck, ui);
        CheckButtonDisabled();
        ui.Background.color = Color.orange;
    }
}