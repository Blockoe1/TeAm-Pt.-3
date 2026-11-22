using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class RaceSelectionScreen : BuckScreen
{
    [SerializeField] private BettingScreen _bettingScreen;
    [SerializeField] private BuckUI _preview;
    [SerializeField] private GameObject _emptyPreview;

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
        List<DeerData> bucks = PlayerManager.Instance.OwnedDeer.Keys.Where(d => d.Age > 1 && d.Age < 8).ToList();

        selected = default;
        InitializeWidgets(bucks);
        _emptyPreview.SetActive(true);
        _preview.gameObject.SetActive(false);
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
            selected.Item2.BuckName.fontStyle = FontStyles.Normal;
        }

        selected = (buck, ui);
        CheckButtonDisabled();
        selected.Item2.BuckName.fontStyle = FontStyles.Underline;

        _emptyPreview.SetActive(false);

        // Copy of InitializeWidget
        var entry = _preview.GetComponent<BuckUI>();
        entry.gameObject.SetActive(true);
        if (entry.BuckName != null) entry.BuckName.text = buck.Name;
        entry.BuckName.ForceMeshUpdate();
        if (entry.BuckName.preferredWidth > entry.BuckName.rectTransform.rect.width) entry.BuckName.characterHorizontalScale = 0.7f;
        if (entry.BuckAppearance != null) entry.BuckAppearance.texture = buck.Controller.Rt;
        entry.Data = buck;
    }
}