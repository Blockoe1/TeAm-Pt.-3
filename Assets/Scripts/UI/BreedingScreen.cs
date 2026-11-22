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
        List<DeerData> bucks = new List<DeerData>();
        for (int i = 0; i < 3; i++)
        {
            bucks.Add(DeerData.NewRandomDeer());
        }

        selected = default;
        InitializeWidgets(bucks);

        // TODO: disable screen navigation
    }

    public override void BuckSelected(DeerData buck, BuckUI ui)
    {
        // TODO: add deer to stables
        _stablesScreen.InitializeStables();
        _screen.SetActive(false);
    }
}
