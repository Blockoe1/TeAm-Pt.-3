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
using System.Linq;
using UnityEngine;

public class DeerPool : Manager
{
    #region Nested Classes
    [System.Serializable]
    private struct PoolData
    {
        [SerializeField] internal int minCount;
        [SerializeField] internal int statBonus;
        [SerializeField] internal Trait.TraitAlignment[] requireTraits;

        internal int StatThreshold => statBonus * 6;
    }
    public enum PoolType
    {
        Good,
        Bad,
        Mid
    }
    #endregion

    [Header("Pools")]
    [SerializeField] private PoolData goodPoolData;
    [SerializeField] private PoolData midPoolData;
    [SerializeField] private PoolData badPoolData;

    private List<DeerData> goodPool;
    private List<DeerData> midPool;
    private List<DeerData> badPool;

    private static DeerPool instance;

    public static DeerPool Instance => instance;

    public override void Initialize()
    {
        SingletonUtil.SetSingleton(ref instance, this);
        goodPool = CreateStartingPool(goodPoolData);
        midPool = CreateStartingPool(midPoolData);
        badPool = CreateStartingPool(badPoolData);
    }
    private List<DeerData> CreateStartingPool(PoolData poolData)
    {
        List<DeerData> pool = new();
        for(int i = 0; i < poolData.minCount; i++)
        {
            pool.Add(CreateDeerForPool(poolData));
        }
        return pool;
    }

    public void AddDeer(DeerData addedDeer)
    {
        GetPoolForDeer(addedDeer, out PoolData _).Add(addedDeer);
    }

    public void RemoveDeer(DeerData removedDeer)
    {
        List<DeerData> pool = GetPoolForDeer(removedDeer, out PoolData data);
        pool.Remove(removedDeer);

        while (pool.Count < data.minCount)
        {
            pool.Add(CreateDeerForPool(data));
        }
    }

    public List<DeerData> GetPool(PoolType pool)
    {
        switch(pool)
        {
            case PoolType.Good:
                return goodPool;
            case PoolType.Bad:
                return badPool;
            case PoolType.Mid:
                return midPool;
            default:
                return null;
        }
    }


    private List<DeerData> GetPoolForDeer(DeerData deer, out PoolData data)
    {
        int totalStats = deer.GetTotalStats();
        if (totalStats >= 60 && HasTraits(deer, goodPoolData.requireTraits))
        {
            data = goodPoolData;
            return goodPool;
        }
        else if (totalStats < 60 && HasTraits(deer, badPoolData.requireTraits))
        {
            data = badPoolData;
            return badPool;
        }
        else
        {
            data = midPoolData;
            return midPool;
        }
    }

    private static bool HasTraits(DeerData deer, Trait.TraitAlignment[] traits)
    {
        List<Trait.TraitAlignment> matchedTraits = traits.ToList();
        foreach(Trait trait in deer.Traits)
        {
            int traitIndex = matchedTraits.IndexOf(trait.Alignment);
            if (traitIndex >= 0)
            {
                matchedTraits.RemoveAt(traitIndex);
            }
        }

        return matchedTraits.Count == 0;
    }

    public List<DeerData> GetDeerFromPools(params PoolType[] pools)
    {
        List<DeerData> deer = new List<DeerData>();
        foreach (PoolType pool in Enum.GetValues(typeof(PoolType)))
        {
            List<DeerData> poolCopy = GetPool(pool).ToList();
            for (int i = 0; i < pools.Where(x => x == pool).Count(); i++)
            {
                int index = UnityEngine.Random.Range(0, poolCopy.Count);
                deer.Add(poolCopy[index]);
                poolCopy.RemoveAt(index);
            }
        }
        return deer;
    }

    private DeerData CreateDeerForPool(PoolData poolData)
    {
        DeerData randomizedDeer = DeerData.NewRandomDeer(poolData.requireTraits);
        foreach (StatType type in Enum.GetValues(typeof(StatType)))
        {
            randomizedDeer.ModifyStat(type, poolData.statBonus);
        }
        randomizedDeer.Age = UnityEngine.Random.Range(2, 5);
        return randomizedDeer;
    }
}
