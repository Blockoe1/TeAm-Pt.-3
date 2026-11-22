using System.Collections.Generic;
using UnityEngine;

public class BreedingScreen : BuckScreen
{
    [SerializeField] private StablesScreen _stablesScreen;

    protected override void Start()
    {
        base.Start();
    }

    public void InitializeBreeding()
    {
        // Fetch new baby data
        List<DeerController> bucks = new List<DeerController>();
        for (int i = 0; i < 3; i++)
        {
            bucks.Add(new DeerController());
            bucks[^1].Data = new DeerData();
            bucks[^1].Data.Name = (i + 1).ToString();
        }

        selected = default;
        InitializeWidgets(bucks);

        // TODO: disable screen navigation
    }

    public override void BuckSelected(DeerController buck, BuckUI ui)
    {
        // TODO: add deer to stables
        _stablesScreen.InitializeStables();
        _screen.SetActive(false);
    }
}
