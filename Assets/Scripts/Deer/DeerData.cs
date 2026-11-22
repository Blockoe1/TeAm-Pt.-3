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
    public const int BASE_TRAIT_NUM = 2;

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

    [field: SerializeReference, ClassDropdown] public Trait[] Traits { get; set; } = new Trait[BASE_TRAIT_NUM];

    [SerializeField]
    private Dictionary<SO_ColorGene, ColorGenome> _colorGenotype = new Dictionary<SO_ColorGene, ColorGenome>();
    [SerializeField]
    private Dictionary<SO_PartGene, PartGenome> _partGenotype = new Dictionary<SO_PartGene, PartGenome>();


    public int Wins { get; private set; }
    public int Races { get; private set; }
    public DeerController Controller { get; set; }

    public int BettingScore 
    { 
        get 
        {
            int score = 0;
            score += Speed;
            score += Acceleration;
            score += Stamina;
            score += Discipline;
            score += Balance;
            
            foreach(Trait trait in Traits)
            {
                if(trait.Alignment == Trait.TraitAlignment.Good)
                {
                    score += 20;
                }
                if(trait.Alignment == Trait.TraitAlignment.Special)
                {
                    score += 10;
                }
            }

            score += Wins * 25;
            score += (Races - Wins) * -5;

            return score;
        }
    }

    public float BetMultiplier
    {
        get
        {
            if (BettingScore >= 110)
            {
                return 1.5f;
            }

            if (BettingScore >= 90)
            {
                return 2f;
            }

            if(BettingScore >= 70)
            {
                return 2.5f;
            }

            if(BettingScore >= 50)
            {
                return 3;
            }

            if(BettingScore >= 30)
            {
                return 3.5f;
            }

            if(BettingScore >= 10)
            {
                return 4;
            }

            return 5f;
        }
    }

    #region GS
    public Dictionary<SO_ColorGene, ColorGenome> ColorGenotype { get => _colorGenotype; set => _colorGenotype = value; }
    public Dictionary<SO_PartGene, PartGenome> PartGenotype { get => _partGenotype; set => _partGenotype = value; }
    #endregion

    public float PreviousBetMultiplier()
    {
        Wins--;
        Races--;
        float previousBet = BetMultiplier;
        Wins++;
        Races++;
        return BetMultiplier;
    }

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

    public void InrementAge()
    {
        AgeTrigger trigger = new AgeTrigger(Age + 1);
        TriggerTraits(trigger, null, this, this, Trait.TriggerFlags.None);
        Age = trigger.Age;
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
        return Mathf.Max(statValue, STAT_MIN);
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
        int goodTraitCount = Traits.Count(x => x != null && x.Alignment == Trait.TraitAlignment.Good);
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
        BuyModifierTrigger trigger = new BuyModifierTrigger(value);
        TraitManager.TraitEvent(trigger, this);
        value = trigger.Price;
            

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

    public void TriggerTraits<T>(T eventTrigger, DeerController triggeredDeerController, DeerData triggeredData, 
        DeerData thisDeer, Trait.TriggerFlags requireFlags) where T : TraitTrigger
    {
        // Trigger each trait in those deer.
        foreach (Trait trait in thisDeer.Traits)
        {
            if (trait is ITriggeredTrait<T> triggered
                && ((trait.TrigFlags & requireFlags) == requireFlags)
                && (trait.TrigFlags.HasFlag(Trait.TriggerFlags.Global) || triggeredData == thisDeer)) // Global traits trigger even when the event was not on this deer.
            {
                triggered.TriggerTrait(eventTrigger, triggeredDeerController, triggeredData, triggeredData == thisDeer);
            }
        }
    }

    public static DeerData NewRandomDeer(params Trait.TraitAlignment[] allowedTraits)
    {
        DeerData data = new DeerData();

        data.Name = DeerNames.GetRandomName();
        foreach(StatType type in Enum.GetValues(typeof(StatType)))
        {
            data.SetStat(type, UnityEngine.Random.Range(STAT_MIN, STAT_MAX));
        }

        if (allowedTraits != null && allowedTraits.Length > 0)
        {
            data.Traits = TraitManager.GetRandomTraits(allowedTraits, BASE_TRAIT_NUM);
        }
        else
        {
            data.Traits = TraitManager.GetRandomTraits(BASE_TRAIT_NUM);
        }

        data.ColorGenotype = NewRandomColorGenotype();
        data.PartGenotype = NewRandomPartGenotype();

        data.TriggerTraits(new BornTrigger(), null, data, data, Trait.TriggerFlags.None);

        return data;
    }

    //Randomize color genotype.
    public static Dictionary<SO_ColorGene, ColorGenome> NewRandomColorGenotype()
    {
        Dictionary<SO_ColorGene, ColorGenome> newGenotype = new Dictionary<SO_ColorGene, ColorGenome>();

        foreach (SO_ColorGene gene in GeneticsMaster.INST.ColorGenes)
            newGenotype.TryAdd(gene, new ColorGenome(gene.GetRandomAllele(), gene.GetRandomAllele()));

        return newGenotype;
    }

    //Randomize part genotype.
    public static Dictionary<SO_PartGene, PartGenome> NewRandomPartGenotype()
    {
        Dictionary<SO_PartGene, PartGenome> newGenotype = new Dictionary<SO_PartGene, PartGenome>();

        foreach (SO_PartGene gene in GeneticsMaster.INST.PartGenes)
            newGenotype.TryAdd(gene, new PartGenome(gene.GetRandomAllele(), gene.GetRandomAllele()));

        return newGenotype;
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