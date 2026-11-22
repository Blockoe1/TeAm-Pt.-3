using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BreedingScreen : BuckScreen
{
    [SerializeField] private StablesScreen _stablesScreen;

    protected override void Start()
    {
        base.Start();
    }

    public void InitializeBreeding(DeerData mom, DeerData dad)
    {
        // Fetch new baby data
        List<DeerData> bucks = new List<DeerData>();
        for (int i = 0; i < 3; i++)
        {
            bucks.Add(GeneticsMaster.INST.Breed(mom.Controller, dad.Controller).Data);
            PlayerManager.Instance.GenerateControllerForNewDeer(bucks[^1]);
        }

        selected = default;
        InitializeWidgets(bucks);

        AllowNavigation(false);
    }

    public override void BuckSelected(DeerData buck, BuckUI ui)
    {
        PlayerManager.Instance.NewBaby(buck);
        _stablesScreen.InitializeStables();
        _screen.SetActive(false);
        AllowNavigation(true);
    }
}
