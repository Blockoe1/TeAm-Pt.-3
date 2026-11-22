using System;
using System.Collections.Generic;
using System.Linq;
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
        List<DeerData> bucks = new List<DeerData>();


        selected = default;
        widgetsToKeep = 2;
        InitializeWidgets(bucks);
    }

    public override void BuckSelected(DeerData buck, BuckUI ui)
    {
        // TODO: add deer to stables
        ReplaceWithBlank(ui);
    }

    public override void Continue()
    {
        
    }
}
