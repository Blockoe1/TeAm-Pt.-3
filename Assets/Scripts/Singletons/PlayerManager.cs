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
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Manager
{
    private const int MAX_OWNED_DEER = 10;
    private List<DeerData> _ownedDeer = new();

    private static PlayerManager instance;

    public IReadOnlyList<DeerData> OwnedDeer => _ownedDeer;
    public static PlayerManager Instance => instance;

    public override void Initialize()
    {
        SingletonUtil.SetSingleton(ref instance, this);
    }

    public void AddDeer(DeerData toAdd)
    {
        if (_ownedDeer.Count <  MAX_OWNED_DEER)
        {
            _ownedDeer?.Add(toAdd);
        }
    }

    public void RemoveDeer(DeerData toRemove)
    {
        _ownedDeer?.Remove(toRemove);
    }
}
