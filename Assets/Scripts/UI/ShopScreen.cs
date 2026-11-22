using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ShopScreen : BuckScreen
{
    [SerializeField] private GameObject purchaseConfirm;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private GameObject background;
    [SerializeField] private FullDeerViewer fullDeerViewer;
    private BuckUI UI;
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
        // Randomize order of bucks in shop.
        System.Random rand = new System.Random();
        bucks = bucks.OrderBy(_ => rand.Next()).ToList();
    
        selected = default;
        widgetsToKeep = 2;
        InitializeWidgets(bucks);
        purchaseConfirm.SetActive(false);
        background.SetActive(false);
    }

    public override void BuckSelected(DeerData buck, BuckUI ui)
    {
        selected = (buck, ui);
        CheckButtonDisabled();

        UI = ui;
        purchaseConfirm.SetActive(true);
        purchaseButton.interactable = PlayerManager.Instance.PurchaseDeer(selected.Item1.Controller, true);
        background.SetActive(true);
        fullDeerViewer.SetDeer(buck, CostParams.Buy);
        fullDeerViewer.ProcessStats(buck);
        AllowNavigation(false);
    }

    public void Confirm()
    {
        bool wasPurchased = PlayerManager.Instance.PurchaseDeer(selected.Item1.Controller);
        if (wasPurchased)
        {
            ReplaceWithBlank(selected.Item2);
        }
        Back();
    }

    public void Back()
    {
        purchaseConfirm.SetActive(false);
        background.SetActive(false);
        selected = default;
        AllowNavigation(true);

    }
}
