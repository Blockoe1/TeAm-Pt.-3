/*****************************************************************************
// File Name : DeerManager.cs
// Author : Arcadia Koederitz
// Creation Date : 10/21/2026
// Last Modified : 10/21/2026
//
// Brief Description : Stores data relevant to the player, such as owned deer.
*****************************************************************************/
using AriUtils;
using MarUtility.ExecutionManagement;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Manager
{
    private const int MAX_OWNED_DEER = 10;
    private Dictionary<DeerData, DeerController> _ownedDeer = new();

    private static PlayerManager instance;

    public Dictionary<DeerData, DeerController> OwnedDeer => _ownedDeer;
    public static PlayerManager Instance => instance;

    public static event Action<DeerData, bool> OnDeerChanged;

    [SerializeField] private GameObject _deerPrefab;

    public override void Initialize()
    {
        SingletonUtil.SetSingleton(ref instance, this);
    }

    public void PurchaseDeer(DeerController toAdd)
    {
        int price = toAdd.Data.CalculatePrice();
        if (_ownedDeer.Count < MAX_OWNED_DEER && MoneyManager.Instance.CanAfford(price))
        {
            _ownedDeer?.Add(toAdd.Data, toAdd);
            DeerPool.Instance.RemoveDeer(toAdd.Data);
            MoneyManager.Instance.SpendMoney(price);
            OnDeerChanged?.Invoke(toAdd.Data, true);
        }
    }

    public void SellDeer(DeerData toSell)
    {
        if (_ownedDeer.ContainsKey(toSell))
        {
            _ownedDeer?.Remove(toSell);
            OnDeerChanged?.Invoke(toSell, false);
            MoneyManager.Instance.EarnMoney(toSell.CalculateSellPrice());
            if (!(toSell.GetAgeBracket() == DeerAge.Retired))
            {
                DeerPool.Instance.AddDeer(toSell);
            }
        }
    }

    public DeerController GenerateControllerForNewDeer(DeerData data)
    {
        var go = Instantiate(_deerPrefab, transform);
        go.name = data.Name;
        var controller = go.GetComponent<DeerController>();
        controller.Data = data;
        controller.Initialize();
        data.Controller = controller;
        return controller;
    }
}
 