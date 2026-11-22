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
        List<DeerData> bucks = new List<DeerData>();
        List<int> goodBadDecider = new List<int>() { 2, 1, 0, 0, 0 };
        for (int i = 0; i < 5; i++)
        {
            int randomGoodBad = UnityEngine.Random.Range(0, goodBadDecider.Count);
            if (goodBadDecider[randomGoodBad] == 2)
            {
                bucks.Add(DeerPool.Instance.GetRandomGoodDeer());
            }
            else if (goodBadDecider[randomGoodBad] == 1)
            {
                bucks.Add(DeerPool.Instance.GetRandomBadDeer());
            }
            else
            {
                bucks.Add(DeerPool.Instance.GetRandomMidDeer());
            }
            goodBadDecider.RemoveAt(randomGoodBad);
        }

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
