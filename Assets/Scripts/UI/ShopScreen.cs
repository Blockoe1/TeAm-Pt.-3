using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopScreen : BuckScreen
{
    [SerializeField] private GameObject purchaseConfirm;
    [SerializeField] private FullDeerViewer fullDeerViewer;
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
        List<DeerData> bucks = DeerPool.Instance.GetDeerFromPools(DeerPool.PoolType.Good, 
            DeerPool.PoolType.Bad, DeerPool.PoolType.Mid, DeerPool.PoolType.Mid, DeerPool.PoolType.Mid);
    
        selected = default;
        widgetsToKeep = 2;
        InitializeWidgets(bucks);
    }

    public override void BuckSelected(DeerData buck, BuckUI ui)
    {
        // TODO: add deer to stables
        ReplaceWithBlank(ui);
        purchaseConfirm.SetActive(true);
        fullDeerViewer.SetDeer(buck.Data);
        fullDeerViewer.ProcessStats(buck.Data);
    }

    public void Confirm()
    {
        purchaseConfirm.SetActive(false);
    }

    public void Back()
    {
        purchaseConfirm.SetActive(false);
    }

    public override void Continue()
    {
        PlayerManager.Instance.PurchaseDeer(selected.Item1);
        ReplaceWithBlank(selected.Item2);
    }
}
