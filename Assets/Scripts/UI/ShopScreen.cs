using System.Collections.Generic;
using UnityEngine;

public class ShopScreen : BuckScreen
{
    protected override void Start()
    {
        base.Start();
        InitializeShop();
    }

    /// <summary>
    /// Run during post-race set-up
    /// </summary>
    public void InitializeShop()
    {
        // Fetch shop data
        List<DeerController> bucks = new List<DeerController>();
        for (int i = 0; i < 5; i++)
        {
            bucks.Add(new DeerController());
            bucks[^1].Data = new DeerData();
            bucks[^1].Data.Name = (i + 1).ToString();
        }

        selected = default;
        widgetsToKeep = 2;
        InitializeWidgets(bucks);
    }

    public override void BuckSelected(DeerController buck, BuckUI ui)
    {
        // TODO: add deer to stables
        ReplaceWithBlank(ui);
        PlayerManager.Instance.AddDeer(buck.Data);
    }
}
