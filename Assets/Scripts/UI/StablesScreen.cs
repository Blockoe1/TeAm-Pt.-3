using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public class StablesScreen : BuckScreen
{
    [SerializeField] private bool _secondStableScreen;
    [SerializeField] private StablesScreen _otherStableScreen;
    [SerializeField] private FullDeerViewer _adjacentViewer;
    [SerializeField] private GameObject _selectedButtons;
    [SerializeField] private GameObject _breedModeButtons;
    [SerializeField] private BreedingScreen _breedingScreen;

    protected override void Start()
    {
        base.Start();
        if (_selectedButtons != null) _selectedButtons.SetActive(false);
        if (_breedModeButtons != null) _breedModeButtons.SetActive(false);
        if (!_secondStableScreen) InitializeStables();
        else _screen.SetActive(false);
        PlayerManager.OnDeerChanged += Handle_OnDeerChanged;
    }

    private void OnDestroy()
    {
        PlayerManager.OnDeerChanged -= Handle_OnDeerChanged;
    }

    private void Handle_OnDeerChanged(DeerData changedDeer, bool wasAdded)
    {
        Debug.Log(changedDeer.Name);
        if (_screen.activeSelf) InitializeStables();
    }

    /// <summary>
    /// Run during post-race set-up and when deer inventory changing
    /// </summary>
    public void InitializeStables(List<DeerData> bucks = null)
    {
        if (bucks == null)
        {
            bucks = PlayerManager.Instance.OwnedDeer.Keys.ToList();
        }
        currentDeer = bucks;

        selected = default;
        _selectedButtons.SetActive(false);
        _adjacentViewer.gameObject.SetActive(false);
        if (_breedModeButtons != null) _breedModeButtons.SetActive(false);
        InitializeWidgets(bucks);
        print("uhh" + gameObject.name);
    }

    public override void BuckSelected(DeerData buck, BuckUI ui)
    {
        if (selected != default)
        {
            selected.Item2.Background.color = Color.wheat;
            if (!_secondStableScreen) CancelBreeding();
        }

        selected = (buck, ui);
        ui.Background.color = Color.orange;
        _adjacentViewer.gameObject.SetActive(true);
        _adjacentViewer.SetDeer(buck, CostParams.None);

        _selectedButtons.SetActive(true);
    }

    /// <summary>
    /// Meaning to initiate breeding in this case
    /// </summary>
    public override void Continue()
    {
        if (_secondStableScreen)
        {
            _breedingScreen.InitializeBreeding(_otherStableScreen.StartBreeding(), selected.Item1);
            _screen.SetActive(false);
            _selectedButtons.SetActive(false);
        }
        else
        {
            List<DeerData> segment = new List<DeerData>();
            foreach (var deer in PlayerManager.Instance.OwnedDeer.Keys) { if (selected.Item1 != deer) segment.Add(deer); }

            _otherStableScreen.InitializeStables(segment);

            _selectedButtons.SetActive(false);
            _breedModeButtons.SetActive(true);
        }
    }

    public DeerData StartBreeding()
    {
        _screen.SetActive(false);
        _breedModeButtons.SetActive(false);
        _adjacentViewer.gameObject.SetActive(false);
        return selected.Item1;
    }

    public void CancelBreeding()
    {
        if (_secondStableScreen)
        {
            _selectedButtons.SetActive(false);
            _screen.gameObject.SetActive(false);
            _adjacentViewer.gameObject.SetActive(false);
        }
        else
        {
            _selectedButtons.SetActive(true);
            _breedModeButtons.SetActive(false);
            _otherStableScreen.CancelBreeding();
        }
    }
}
