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
    private List<DeerData> _ownedDeer = new();

    private static PlayerManager instance;

    public List<DeerData> OwnedDeer => _ownedDeer;
    public static PlayerManager Instance => instance;

    public static event Action<DeerData, bool> OnDeerChanged;

    public override void Initialize()
    {
        SingletonUtil.SetSingleton(ref instance, this);
    }

    public void PurchaseDeer(DeerData toAdd)
    {
        int price = toAdd.CalculatePrice();
        if (_ownedDeer.Count < MAX_OWNED_DEER && MoneyManager.Instance.CanAfford(price))
        {
            _ownedDeer?.Add(toAdd);
            DeerPool.Instance.RemoveDeer(toAdd);
            MoneyManager.Instance.SpendMoney(price);
            OnDeerChanged?.Invoke(toAdd, true);
        }
    }

    public void SellDeer(DeerData toSell)
    {
        if (_ownedDeer.Contains(toSell))
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
}
 