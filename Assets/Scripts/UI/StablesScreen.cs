using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class StablesScreen : BuckScreen
{
    [SerializeField] private bool _secondStableScreen;
    [SerializeField] private StablesScreen _otherStableScreen;
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
    }

    /// <summary>
    /// Run during post-race set-up and when deer inventory changing
    /// </summary>
    public void InitializeStables(List<DeerData> bucks = null)
    {
        // Fetch stable data
        // If this is the second screen, pass in every stable deer except the first one,
        // or disable the one you've already selected
        if (bucks == null)
        {
            bucks = new List<DeerData>();
            for (int i = 0; i < 8; i++)
            {
                bucks.Add(DeerData.NewRandomDeer());
                //bucks[^1].Data = DeerData.NewRandomDeer();
                //bucks[^1].Data.Name = (i + 1).ToString();
            }
        }
        currentDeer = bucks;

        selected = default;
        _selectedButtons.SetActive(false);
        if (_breedModeButtons != null) _breedModeButtons.SetActive(false);
        InitializeWidgets(bucks);
    }

    public override void BuckSelected(DeerData buck, BuckUI ui)
    {
        if (selected != default)
        {
            selected.Item2.Background.color = Color.wheat;
        }

        selected = (buck, ui);
        ui.Background.color = Color.orange;

        _selectedButtons.SetActive(true);
    }

    /// <summary>
    /// Meaning to initiate breeding in this case
    /// </summary>
    public override void Continue()
    {
        if (_secondStableScreen)
        {
            _breedingScreen.InitializeBreeding();
            _screen.SetActive(false);
            _selectedButtons.SetActive(false);
            _otherStableScreen.StartBreeding();
        }
        else
        {
            List<DeerData> segment = new List<DeerData>();
            foreach (var deer in currentDeer) { if (selected.Item1 != deer) segment.Add(deer); }

            //solution to having fake deer rn
            foreach (var deer in currentDeer) { segment.Add(deer); }

            _otherStableScreen.InitializeStables(segment);

            _selectedButtons.SetActive(false);
            _breedModeButtons.SetActive(true);
        }
    }

    public void StartBreeding()
    {
        _screen.SetActive(false);
        _breedModeButtons.SetActive(false);
    }

    public void CancelBreeding()
    {
        if (_secondStableScreen)
        {
            _selectedButtons.SetActive(false);
            _screen.gameObject.SetActive(false);
        }
        else
        {
            _selectedButtons.SetActive(true);
            _breedModeButtons.SetActive(false);
            _otherStableScreen.CancelBreeding();
        }
    }
}
