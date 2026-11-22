/*****************************************************************************
// File Name : DeerData.cs
// Author : Arcadia Koederitz
// Creation Date : 10/04/2026
// Last Modified : 10/04/2026
//
// Brief Description : Holds data related to a deer.
*****************************************************************************/
using AriUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class DeerData
{
    private const int BASE_TRAIT_NUM = 2;

    public const int STAT_MIN = 1;
    public const int STAT_MAX = 20;

    [field: SerializeField] public string Name { get; set; }
    #region Stats
    [field: SerializeField, Range(STAT_MIN, STAT_MAX)] public int Speed { get; set; } = 1;
    [field: SerializeField, Range(STAT_MIN, STAT_MAX)] public int Acceleration { get; set; } = 1;
    [field: SerializeField, Range(STAT_MIN, STAT_MAX)] public int Stamina { get; set; } = 1;
    [field: SerializeField, Range(STAT_MIN, STAT_MAX)] public int Balance { get; set; } = 1;
    [field: SerializeField, Range(STAT_MIN, STAT_MAX)] public int Discipline { get; set; } = 1;
    [field: SerializeField, Range(STAT_MIN, STAT_MAX)] public int Charisma { get; set; } = 1;
    #endregion

    [field: SerializeField, Min(1)] public int Age { get; set; } = 1;

    [field: SerializeReference, ClassDropdown] public Trait[] Traits { get; private set; } = new Trait[BASE_TRAIT_NUM];

    [SerializeField]
    private Dictionary<SO_ColorGene, ColorGenotype> _colorGenotype;


    public int Wins { get; private set; }
    public int Races { get; private set; }
    public DeerController Controller { get; set; }

    #region GS
    public Dictionary<SO_ColorGene, ColorGenotype> ColorGenotype { get => _colorGenotype; set => _colorGenotype = value; }
    #endregion

    public void LogRace(bool wasWin)
    {
        Races++;
        if (wasWin)
        {
            Wins++;
        }
    }

    public DeerAge GetAgeBracket()
    {
        if(Age >= 11)
        {
            return DeerAge.Dead;
        }
        else if (Age >= 8)
        {
            return DeerAge.Retired;
        }
        else if (Age >= 5)
        {
            return DeerAge.Breeder;
        }
        else if (Age >= 2)
        {
            return DeerAge.Racer;
        }
        else
        {
            return DeerAge.Baby;
        }
    }

    #region Stats
    public void SetStat(StatType stat, int value)
    {
        value = Mathf.Clamp(value, STAT_MIN, STAT_MAX);
        switch (stat)
        {
            case StatType.Speed:
                Speed = value; break;
            case StatType.Acceleration:
                Acceleration = value; break;
            case StatType.Stamina:
                Stamina = value; break;
            case StatType.Balance:
                Balance = value; break;
            case StatType.Discipline:
                Discipline = value; break;
            case StatType.Charisma:
                Charisma = value; break;
        }
    }

    public void ModifyStat(StatType stat, int value)
    {
        switch (stat)
        {
            case StatType.Speed:
                Speed = Mathf.Clamp(Speed + value, STAT_MIN, STAT_MAX); break;
            case StatType.Acceleration:
                Acceleration = Mathf.Clamp(Acceleration + value, STAT_MIN, STAT_MAX); break;
            case StatType.Stamina:
                Stamina = Mathf.Clamp(Stamina + value, STAT_MIN, STAT_MAX); break;
            case StatType.Balance:
                Balance = Mathf.Clamp(Balance + value, STAT_MIN, STAT_MAX); break;
            case StatType.Discipline:
                Discipline = Mathf.Clamp(Discipline + value, STAT_MIN, STAT_MAX); break;
            case StatType.Charisma:
                Charisma = Mathf.Clamp(Charisma + value, STAT_MIN, STAT_MAX); break;
        }
    }

    public int GetStat(StatType stat)
    {
        switch (stat)
        {
            case StatType.Speed:
                return Speed;
            case StatType.Acceleration:
                return Acceleration;
            case StatType.Stamina:
                return Stamina;
            case StatType.Balance:
                return Balance;
            case StatType.Discipline:
                return Discipline;
            case StatType.Charisma:
                return Charisma;
            default:
                return 0;
        }
    }

    public int ApplyTraits(StatType stat, int value)
    {
        if (Traits == null || Traits.Length == 0)
        {
            return value;
        }
        int statValue = value;
        Trait[] statTraits = Traits.Where(x => x != null).OrderBy(x =>x.Priority).ToArray();
        foreach (Trait trait in statTraits)
        {
            statValue = trait.ModifyStat(stat, statValue);
        }
        return statValue;
    }

    public int GetTotalStats()
    {
        int total = 0;
        foreach (StatType type in Enum.GetValues(typeof(StatType)))
        {
            total += GetStat(type);
        }
        return total;
    }
    #endregion

    #region Purchasing
    public float CalculateBaseValue()
    {
        int goodTraitCount = Traits.Count(x => x.Alignment == Trait.TraitAlignment.Good);
        return GetTotalStats() * ((goodTraitCount + 1f) / 2f);
    }

    public int CalculatePrice()
    {
        float value = CalculateBaseValue();

        if (GetAgeBracket() == DeerAge.Retired)
        {
            value *= 0.5f;
        }
        else if (GetAgeBracket() == DeerAge.Retired)
        {
            value *= 1.25f;
        }

        // TODO: Call Trait CalcualtePriceTrait
            

        return Mathf.RoundToInt(value);
    }

    public int CalculateSellPrice()
    {
        float value = CalculateBaseValue();

        if (GetAgeBracket() == DeerAge.Retired)
        {
            value *= 0.5f;
        }
        else if (GetAgeBracket() == DeerAge.Retired)
        {
            value *= 1.25f;
        } 

        if (Traits != null && Traits.Length > 0)
        {
            Trait[] sellTraits = Traits.Where(x => x != null).OrderBy(x => x.Priority).ToArray();
            foreach (Trait trait in sellTraits)
            {
                value = trait.ModifySellPrice(value);
            }
        }

        return Mathf.RoundToInt(value);
    }
    #endregion

    public static DeerData NewRandomDeer(params Trait.TraitAlignment[] requireddTraits)
    {
        DeerData data = new DeerData();

        data.Name = DeerNames.GetRandomName();
        foreach(StatType type in Enum.GetValues(typeof(StatType)))
        {
            data.SetStat(type, UnityEngine.Random.Range(STAT_MIN, STAT_MAX));
        }

        data.Traits = new Trait[BASE_TRAIT_NUM];
        for(int i = 0; i < BASE_TRAIT_NUM; i++)
        {
            if (requireddTraits != null && requireddTraits.Length > i)
            {
                data.Traits[i] = TraitManager.GetRandomTrait(requireddTraits[i]);
            }
            else
            {
                data.Traits[i] = TraitManager.GetRandomTrait();          
            }
        }

        data.ColorGenotype = NewRandomColorGenotype();

        return data;
    }

    //Randomize color genotype.
    public static Dictionary<SO_ColorGene, ColorGenotype> NewRandomColorGenotype()
    {
        Dictionary<SO_ColorGene, ColorGenotype> newColorGenotype = new Dictionary<SO_ColorGene, ColorGenotype>();

        foreach (SO_ColorGene gene in GeneticsMaster.INST.ColorGenes)
            newColorGenotype.TryAdd(gene, new ColorGenotype(gene.GetRandomAllele(), gene.GetRandomAllele()));

        return newColorGenotype;
    }
}

public enum DeerAge
{
    Baby,
    Racer,
    Breeder,
    Retired,
    Dead
}